using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using StockManagement.Application.Features.Query;
using StockManagement.Application.Services;

namespace StockManagement.Application.Features.EventHandlers.Query
{
    public class GetActiveStockAlertsQueryHandler : IRequestHandler<GetActiveStockAlertsQuery, Result>
    {
        private readonly IStockAlertService _service;
        private readonly ILoggerService<GetActiveStockAlertsQueryHandler> _logger;

        public GetActiveStockAlertsQueryHandler(IStockAlertService service, ILoggerService<GetActiveStockAlertsQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetActiveStockAlertsQuery request, CancellationToken cancellationToken)
        {
            var alerts = await _service.GetActiveAlertsAsync();
            return Result.Success(alerts);
        }
    }
}