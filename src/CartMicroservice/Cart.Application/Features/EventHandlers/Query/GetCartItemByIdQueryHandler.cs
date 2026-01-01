using CartMicroservice.Application.Contracts;
using CartMicroservice.Application.Features.Query;
using CartMicroservice.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.EventHandlers.Query
{
    public class GetCartItemByIdQueryHandler : IRequestHandler<GetCartItemByIdQuery, Result>
    {
        private readonly ICartItemService _cartItemService;
        private readonly ILoggerService<GetCartItemByIdQueryHandler> _logger;

        public GetCartItemByIdQueryHandler(ICartItemService cartItemService, ILoggerService<GetCartItemByIdQueryHandler> logger)
        {
            _cartItemService = cartItemService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetCartItemByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _cartItemService.GetItemByIdAsync(request.CartItemId);
                if (item is null)
                    return Result.Failure(new FailureResponse("NotFound", "Cart item not found"));

                var dto = item.Adapt<AddCartDto>();
                return Result.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetCartItemByIdQueryHandler: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("Error", "Failed to retrieve cart item"));
            }
        }
    }
}