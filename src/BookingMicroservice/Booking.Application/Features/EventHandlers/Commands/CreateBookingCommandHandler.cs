using Booking.Application.Contracts;
using Booking.Application.Service;
using BookingMicroservice.Application.Features.Commands;
using BookingMicroservice.Application.Service;
using BookingMicroservice.Domain.Entities;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System.Text.Json;

namespace BookingMicroservice.Application.Features.EventHandlers.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result>
    {
        private readonly IBookingService _bookingService;
        private readonly IOrderClient orderClient;
        private readonly IPaymentClient paymentClient;
        private readonly ILoggerService<CreateBookingCommandHandler> _logger;

        public CreateBookingCommandHandler(ILoggerService<CreateBookingCommandHandler> logger, IBookingService bookingService, IOrderClient orderClient, IPaymentClient paymentClient)
        {
            _bookingService = bookingService;
            this.orderClient = orderClient;
            this.paymentClient = paymentClient;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new BookingMaster {
                EntityId = request.EntityId,
                EntityType = request.EntityType,
                ProductName = request.Name,
                UserId = "1",
                Date = request.Date,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Status = BookingStatus.Pending,
                Notes = request.Notes,
                BookingOptionsJson = JsonSerializer.Serialize(request.BookingOptions), 
            };
            try
            {
                string bookingRefNum = await _bookingService.CreateAsync(booking);
                if (bookingRefNum == null) {
                    return Result.Failure(new FailureResponse("BOKING_CREATION_FAILED", "Booking creation failed"));
                }
                
                OrderResponse? order = await orderClient.AddOrderAsync(bookingRefNum);
                if (order == null)
                {
                    return Result.Failure(new FailureResponse("ORDER_CREATION_FAILED", "Order creation failed"));
                }

                PaymentResponse? payment = await paymentClient.Payment(order.OrderNumber, booking.UserId, order.GrandTotal);

                if(payment == null)
                {
                    bool paymentStatus = await orderClient.UpdateStatusOrderAsync(order.OrderNumber, "Success");
                    return Result.Failure(new FailureResponse("PAYMENT_CREATION_FAILED", "Payment creation failed"));
                }
                booking.Status = Enum.Parse<BookingStatus>("Confirmed", true);
                await _bookingService.UpdateStatusBookingAsync(booking);
                await orderClient.UpdateStatusOrderAsync(order.OrderNumber, "Confirmed");
                return Result.Success(payment);
            }
            catch (Exception)
            {
                _logger.LogWarning("Booking creation failed for request {@Request}", request);
                return Result.Failure(new FailureResponse("BOKING_CREATION_FAILED", "Booking creation failed"));
            }
        }
    }

}