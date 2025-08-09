using Mapster;
using MediatR;
using PaymentMicroservice.Application.Features.Commands;
using PaymentMicroservice.Application.Services;
using PaymentMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.EventHandlers.Commands
{
    public class UpdatePaymentTransactionCommandHandler : IRequestHandler<UpdatePaymentTransactionCommand, Result>
{
    private readonly IPaymentService _paymentService;
    private readonly ILoggerService<UpdatePaymentTransactionCommandHandler> _logger;

    public UpdatePaymentTransactionCommandHandler(IPaymentService paymentService, ILoggerService<UpdatePaymentTransactionCommandHandler> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdatePaymentTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = request.TransactionDto.Adapt<PaymentTransaction>();
            var success = await _paymentService.UpdatePaymentTransactionAsync(transaction);

            return success
                ? Result.Success()
                : Result.Failure(new FailureResponse("Failed", "Update operation failed."));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in UpdatePaymentTransactionCommandHandler", ex);
            return Result.Failure(new FailureResponse("Exception", ex.Message));
        }
    }
}

}