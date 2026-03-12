using MediatR;
using Order.Application.Features.Commands;
using Order.Application.Services;
using Order.Domain.Enums;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Application.Features.EventHandlers.Commands
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Result>
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService<UpdateOrderStatusCommandHandler> _logger;

        public UpdateOrderStatusCommandHandler(IOrderService orderService, ILoggerService<UpdateOrderStatusCommandHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderEntity = await _orderService.GetOrderByOrderNumberAsync(request.OrderNumber);

                if (orderEntity == null)
                {
                    return Result.Failure(new FailureResponse("NOT_FOUND", $"Order with number '{request.OrderNumber}' was not found."));
                }

                orderEntity.Status = Enum.Parse<OrderStatus>(request.Status, true);

                var success = await _orderService.UpdateOrderAsync(orderEntity);

                if (!success)
                {
                    return Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update order status."));
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating order status for OrderNumber: {OrderNumber}", request.OrderNumber);

                return Result.Failure(new FailureResponse("EXCEPTION", "An error occurred while updating the order status."));
            }
        }
    }
}
