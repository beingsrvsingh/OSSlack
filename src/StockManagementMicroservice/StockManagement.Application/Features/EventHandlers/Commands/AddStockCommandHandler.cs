using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using StockManagement.Application.Features.Commands;
using StockManagement.Application.Services;

namespace StockManagement.Application.Features.EventHandlers.Commands
{
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, Result>
    {
        private readonly IStockService _service;
        private readonly ILoggerService<AddStockCommandHandler> _logger;

        public AddStockCommandHandler(IStockService service, ILoggerService<AddStockCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var success = await _service.AddStockAsync(request.Stock);
            return success ? Result.Success() : Result.Failure("Failed to add stock.");
        }
    }

}