using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using StockManagement.Application.Services;

namespace StockManagement.Application.Features.EventHandlers.Commands
{
    public class AddWarehouseCommandHandler : IRequestHandler<AddWarehouseCommand, Result>
{
    private readonly IWarehouseService _service;
    private readonly ILoggerService<AddWarehouseCommandHandler> _logger;

    public AddWarehouseCommandHandler(IWarehouseService service, ILoggerService<AddWarehouseCommandHandler> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result> Handle(AddWarehouseCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.AddAsync(request.Warehouse);
        return result ? Result.Success() : Result.Failure("Failed to add warehouse");
    }
}
}