using BookingMicroservice.Application.Features.Commands;
using BookingMicroservice.Application.Service;
using BookingMicroservice.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using System.Text.Json;

namespace BookingMicroservice.Application.Features.EventHandlers.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result>
    {
        private readonly IBookingService _bookingService;
        private readonly ILoggerService<CreateBookingCommandHandler> _logger;

        public CreateBookingCommandHandler(ILoggerService<CreateBookingCommandHandler> logger, IBookingService astrologerService)
        {
            _bookingService = astrologerService;
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
                string bookingId = await _bookingService.CreateAsync(booking);
                return Result.Success(new { Message = "Booking created successfully.", Data = bookingId });
            }
            catch (Exception)
            {
                _logger.LogWarning("Booking creation failed for request {@Request}", request);
                return Result.Failure(new FailureResponse("ASTRO_CREATION_FAILED", "Booking creation failed"));
            }
        }
    }

}