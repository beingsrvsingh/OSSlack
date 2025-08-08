using Mapster;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Features.Query;
using Order.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Application.Features.EventHandlers.Query
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result>
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService<GetOrderByIdQueryHandler> _logger;

        public GetOrderByIdQueryHandler(IOrderService orderService, ILoggerService<GetOrderByIdQueryHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(request.OrderId);
                if (order is null)
                    return Result.Failure(new FailureResponse("NotFound", "Order not found"));

                var dto = order.Adapt<OrderDto>();
                return Result.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetOrderByIdQueryHandler", ex);
                return Result.Failure(new FailureResponse("Error", "Failed to fetch order"));
            }
        }
    }

}