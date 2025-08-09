using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using StockManagement.Application.Features.Query;
using StockManagement.Application.Services;

namespace StockManagement.Application.Features.EventHandlers.Query
{
    public class GetStockTransactionsQueryHandler : IRequestHandler<GetStockTransactionsQuery, Result>
    {
        private readonly IStockTransactionService _service;
        private readonly ILoggerService<GetStockTransactionsQueryHandler> _logger;

        public GetStockTransactionsQueryHandler(IStockTransactionService service, ILoggerService<GetStockTransactionsQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetStockTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _service.GetByStockIdAsync(request.StockId);
            return Result.Success(transactions);
        }
    }
}