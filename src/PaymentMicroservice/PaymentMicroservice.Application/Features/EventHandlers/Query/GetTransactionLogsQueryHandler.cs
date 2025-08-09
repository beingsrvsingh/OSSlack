using Mapster;
using MediatR;
using PaymentMicroservice.Application.Contracts;
using PaymentMicroservice.Application.Features.Query;
using PaymentMicroservice.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.EventHandlers.Query
{
    public class GetTransactionLogsQueryHandler : IRequestHandler<GetTransactionLogsQuery, Result>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILoggerService<GetTransactionLogsQueryHandler> _logger;

        public GetTransactionLogsQueryHandler(IPaymentService paymentService, ILoggerService<GetTransactionLogsQueryHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTransactionLogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var logs = await _paymentService.GetTransactionLogsAsync(request.TransactionId);
                var result = logs.Adapt<IEnumerable<PaymentTransactionLogDto>>();
                return Result.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetTransactionLogsQueryHandler", ex);
                return Result.Failure(new FailureResponse("Exception", ex.Message));
            }
        }
    }

}