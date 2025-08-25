using Mapster;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Features.Query;
using Order.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Application.Features.EventHandlers.Query
{
    public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, Result>
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService<ListOrdersQueryHandler> _logger;

        public ListOrdersQueryHandler(IOrderService orderService, ILoggerService<ListOrdersQueryHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<Result> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = request.userId is null
                    ? await _orderService.GetAllOrdersAsync()
                    : await _orderService.GetOrdersByUserIdAsync(request.userId);

                var orderDtos = OrderDto.ToResponseDtoList(orders);
                return Result.Success(orderDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in ListOrdersQueryHandler", ex);
                return Result.Failure(new FailureResponse("Error", "Failed to list orders"));
            }
        }
    }

}