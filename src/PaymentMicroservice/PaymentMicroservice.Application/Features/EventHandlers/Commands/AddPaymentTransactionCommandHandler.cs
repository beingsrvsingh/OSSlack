using Mapster;
using MediatR;
using PaymentMicroservice.Application.Features.Commands;
using PaymentMicroservice.Application.Services;
using PaymentMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.EventHandlers.Commands
{
    public class AddPaymentTransactionCommandHandler : IRequestHandler<AddPaymentTransactionCommand, Result>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILoggerService<AddPaymentTransactionCommandHandler> _logger;

        public AddPaymentTransactionCommandHandler(IPaymentService paymentService, ILoggerService<AddPaymentTransactionCommandHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<Result> Handle(AddPaymentTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = request.TransactionDto.Adapt<PaymentTransaction>();
                var success = await _paymentService.AddPaymentTransactionAsync(transaction);

                return success
                    ? Result.Success()
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