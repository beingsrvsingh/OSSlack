using MediatR;
using PaymentMicroservice.Application.Features.Commands;
using PaymentMicroservice.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.EventHandlers.Commands
{
    public class DeletePaymentTransactionCommandHandler : IRequestHandler<DeletePaymentTransactionCommand, Result>
{
    private readonly IPaymentService _paymentService;
    private readonly ILoggerService<DeletePaymentTransactionCommandHandler> _logger;

    public DeletePaymentTransactionCommandHandler(IPaymentService paymentService, ILoggerService<DeletePaymentTransactionCommandHandler> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    public async Task<Result> Handle(DeletePaymentTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var success = await _paymentService.DeletePaymentTransactionAsync(request.TransactionId);

            return success
                ? Result.Success()
                : Result.Failure(new FailureResponse("Failed", "Delete operation failed."));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in DeletePaymentTransactionCommandHandler", ex);
            return Result.Failure(new FailureResponse("Exception", ex.Message));
        }
    }
}

}