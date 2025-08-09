using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using StockManagement.Application.Features.Commands;
using StockManagement.Application.Services;

namespace StockManagement.Application.Features.EventHandlers.Commands
{
    public class AddStockAdjustmentCommandHandler : IRequestHandler<AddStockAdjustmentCommand, Result>
{
    private readonly IStockAdjustmentService _service;
    private readonly ILoggerService<AddStockAdjustmentCommandHandler> _logger;

    public AddStockAdjustmentCommandHandler(IStockAdjustmentService service, ILoggerService<AddStockAdjustmentCommandHandler> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result> Handle(AddStockAdjustmentCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.AddAdjustmentAsync(request.Adjustment);
        return result ? Result.Success() : Result.Failure("Failed to add adjustment");
    }
}

}