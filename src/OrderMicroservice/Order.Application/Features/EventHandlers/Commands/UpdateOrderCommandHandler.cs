using Mapster;
using MediatR;
using Order.Application.Features.Commands;
using Order.Application.Services;
using Order.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Application.Features.EventHandlers.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result>
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderService orderService, ILoggerService<UpdateOrderCommandHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderEntity = request.Order.Adapt<OrderHeader>();
                var success = await _orderService.UpdateOrderAsync(orderEntity);
                return success ? Result.Success() : Result.Failure(new FailureResponse("Error", "Failed to update order"));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in UpdateOrderCommandHandler", ex);
                return Result.Failure(new FailureResponse("Error", "Exception occurred while updating order"));
            }
        }
    }

}