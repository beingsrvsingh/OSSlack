using Mapster;
using MediatR;
using Payment.Application.Services;
using PaymentMicroservice.Application.Features.Commands;
using PaymentMicroservice.Application.Services;
using PaymentMicroservice.Domain.Entities;
using PaymentMicroservice.Domain.Enums;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.EventHandlers.Commands
{
    public class AddPaymentTransactionCommandHandler : IRequestHandler<AddPaymentTransactionCommand, Result>
    {
        private readonly IPaymentService _paymentService;
        private readonly ICashfreeService cashfreeService;
        private readonly ILoggerService<AddPaymentTransactionCommandHandler> _logger;

        public AddPaymentTransactionCommandHandler(IPaymentService paymentService, ICashfreeService cashfreeService, ILoggerService<AddPaymentTransactionCommandHandler> logger)
        {
            _paymentService = paymentService;
            this.cashfreeService = cashfreeService;
            _logger = logger;
        }

        public async Task<Result> Handle(AddPaymentTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderId = Guid.NewGuid().ToString("N");

                var payment = await cashfreeService.CreateOrderAsync(
                    orderId,
                    request.TransactionDto.Amount,
                    request.TransactionDto.UserId,
                    request.TransactionDto.Email,
                    request.TransactionDto.Phone
                );

                var transaction = request.TransactionDto.Adapt<PaymentTransaction>();
                transaction.OrderId = orderId;
                transaction.Status = PaymentStatus.Pending;

                var success = await _paymentService.AddPaymentTransactionAsync(transaction);

                return success
                    ? Result.Success(payment)
                    : Result.Failure(new FailureResponse("Failed", "Failed to create payment transaction."));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in AddPaymentTransactionCommandHandler", ex);
                return Result.Failure(new FailureResponse("Exception", ex.Message));
            }
        }
    }

}