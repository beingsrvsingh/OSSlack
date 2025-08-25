using Mapster;
using MediatR;
using Order.Application.Features.Commands;
using Order.Application.Services;
using Order.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Application.Features.EventHandlers.Commands
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Result>
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService<AddOrderCommandHandler> _logger;

        public AddOrderCommandHandler(IOrderService orderService, ILoggerService<AddOrderCommandHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<Result> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderEntity = request.Adapt<OrderHeader>();
                var success = await _orderService.AddOrderAsync(orderEntity);
                return success ? Result.Success() : Result.Failure(new FailureResponse("Error", "Failed to add order"));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in AddOrderCommandHandler", ex);
                return Result.Failure(new FailureResponse("Error", "Exception occurred while adding order"));
            }
        }
    }

}