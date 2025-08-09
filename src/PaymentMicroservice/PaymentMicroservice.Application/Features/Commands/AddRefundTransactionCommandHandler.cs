using Mapster;
using MediatR;
using PaymentMicroservice.Application.Services;
using PaymentMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.Commands
{
    public class AddRefundTransactionCommandHandler : IRequestHandler<AddRefundTransactionCommand, Result>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILoggerService<AddRefundTransactionCommandHandler> _logger;

        public AddRefundTransactionCommandHandler(IPaymentService paymentService, ILoggerService<AddRefundTransactionCommandHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<Result> Handle(AddRefundTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var refund = request.RefundDto.Adapt<RefundTransaction>();
                var success = await _paymentService.AddRefundTransactionAsync(refund);

                return success
                    ? Result.Success()
                    : Result.Failure(new FailureResponse("Failed", "Failed to create refund."));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddRefundTransactionCommandHandler", ex);
                return Result.Failure(new FailureResponse("Exception", ex.Message));
            }
        }
    }

}