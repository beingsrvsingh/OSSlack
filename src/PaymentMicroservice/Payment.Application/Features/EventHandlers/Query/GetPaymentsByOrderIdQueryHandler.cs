using Mapster;
using MediatR;
using PaymentMicroservice.Application.Features.Query;
using PaymentMicroservice.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Payment.Application.Features.EventHandlers.Query
{
    public class GetPaymentsByOrderIdQueryHandler : IRequestHandler<GetPaymentsByOrderIdQuery, Result>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILoggerService<GetPaymentsByOrderIdQueryHandler> _logger;

        public GetPaymentsByOrderIdQueryHandler(IPaymentService paymentService, ILoggerService<GetPaymentsByOrderIdQueryHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetPaymentsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var payments = await _paymentService.GetPaymentTransactionByOrderIdAsync(request.OrderId);
                if (payments == null)
                {
                    return Result.Failure(new FailureResponse("PAYMENT_NOT_FOUND", "Payment details not found"));
                }             
                return Result.Success(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPaymentsByOrderIdQuery", ex);
                return Result.Failure(new FailureResponse("Exception", ex.Message));
            }
        }
    }
}