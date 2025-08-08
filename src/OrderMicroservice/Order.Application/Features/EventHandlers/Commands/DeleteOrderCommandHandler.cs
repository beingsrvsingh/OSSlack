using Mapster;
using MediatR;
using Order.Application.Features.Commands;
using Order.Application.Services;
using Order.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Application.Features.EventHandlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result>
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderService orderService, ILoggerService<DeleteOrderCommandHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _orderService.DeleteOrderAsync(request.OrderId);
                return success ? Result.Success() : Result.Failure(new FailureResponse("Error", "Failed to delete order"));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in DeleteOrderCommandHandler", ex);
                return Result.Failure(new FailureResponse("Error", "Exception occurred while deleting order"));
            }
        }
    }

}