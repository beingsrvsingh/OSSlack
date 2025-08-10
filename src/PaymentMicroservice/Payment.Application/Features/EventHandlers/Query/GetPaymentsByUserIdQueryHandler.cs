using Mapster;
using MediatR;
using PaymentMicroservice.Application.Contracts;
using PaymentMicroservice.Application.Features.Query;
using PaymentMicroservice.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.EventHandlers.Query
{
    public class GetPaymentsByUserIdQueryHandler : IRequestHandler<GetPaymentsByUserIdQuery, Result>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILoggerService<GetPaymentsByUserIdQueryHandler> _logger;

        public GetPaymentsByUserIdQueryHandler(IPaymentService paymentService, ILoggerService<GetPaymentsByUserIdQueryHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetPaymentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var payments = await _paymentService.GetPaymentsByUserIdAsync(request.UserId);
                var data = payments.Adapt<IEnumerable<PaymentTransactionDto>>();
                return Result.Success(data);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPaymentsByUserIdQueryHandler", ex);
                return Result.Failure(new FailureResponse("Exception", ex.Message));
            }
        }
    }

}