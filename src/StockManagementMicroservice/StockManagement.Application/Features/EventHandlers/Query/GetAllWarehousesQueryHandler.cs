using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using StockManagement.Application.Features.Query;
using StockManagement.Application.Services;

namespace StockManagement.Application.Features.EventHandlers.Query
{
    public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, Result>
    {
        private readonly IWarehouseService _service;
        private readonly ILoggerService<GetAllWarehousesQueryHandler> _logger;

        public GetAllWarehousesQueryHandler(IWarehouseService service, ILoggerService<GetAllWarehousesQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _service.GetAllAsync();
                return Result.Success(data);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to fetch warehouses", ex);
                return Result.Failure("Internal error");
            }
        }
    }
}