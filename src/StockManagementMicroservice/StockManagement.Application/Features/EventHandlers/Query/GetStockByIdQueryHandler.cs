using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using StockManagement.Application.Features.Query;
using StockManagement.Application.Services;

namespace StockManagement.Application.Features.EventHandlers.Query
{
    public class GetStockByIdQueryHandler : IRequestHandler<GetStockByIdQuery, Result>
    {
        private readonly IStockService _service;
        private readonly ILoggerService<GetStockByIdQueryHandler> _logger;

        public GetStockByIdQueryHandler(IStockService service, ILoggerService<GetStockByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stock = await _service.GetByIdAsync(request.StockId);
                return stock is null
                    ? Result.Failure("Stock not found")
                    : Result.Success(stock);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching stock", ex);
                return Result.Failure("Internal error");
            }
        }
    }
}