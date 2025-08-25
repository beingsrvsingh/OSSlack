using Mapster;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Features.Query;
using Order.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Cryptography;
using Shared.Utilities.Response;

namespace Order.Application.Features.EventHandlers.Query
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailQuery, Result>
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService<GetOrderDetailsQueryHandler> _logger;

        public GetOrderDetailsQueryHandler(IOrderService orderService, ILoggerService<GetOrderDetailsQueryHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var decryptedString = Cryptography.DecryptString(request.OrderId);

                if (!int.TryParse(decryptedString, out int orderId))
                {
                    return Result.Failure(new FailureResponse("NotFound", "Order not found"));
                }
                
                var order = await _orderService.GetOrderDetailsAsync(orderId);
                if (order is null)
                    return Result.Failure(new FailureResponse("NotFound", "Order not found"));
                
                return Result.Success(order);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetOrderByIdQueryHandler", ex);
                return Result.Failure(new FailureResponse("Error", "Failed to fetch order"));
            }
        }
    }

}