using Cart.Application.Contracts;
using Cart.Application.Features.Query;
using CartMicroservice.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Cart.Application.Features.EventHandlers.Query
{
    public class GetOrderCartByUserIdQueryHandler : IRequestHandler<GetOrderCartByUserIdQuery, Result>
    {
        private readonly ICartService _cartService;
        private readonly ILoggerService<GetOrderCartByUserIdQueryHandler> _logger;

        public GetOrderCartByUserIdQueryHandler(ICartService cartService, ILoggerService<GetOrderCartByUserIdQueryHandler> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }
        public async Task<Result> Handle(GetOrderCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _cartService.GetCartByUserIdAsync(request.UserId);

                if (cart == null)
                {                    
                    return Result.Success();
                }

                List<OrderCartItemDto> cartItems = new List<OrderCartItemDto>();

                foreach (var item in cart.CartItems) {
                    OrderCartItemDto cartItem = new();
                    cartItem.ProductId = item.ProductVariantId;
                    cartItem.ProductUrl = item.ImageUrl;
                    cartItem.Quantity = item.Quantity;
                    cartItem.UnitPrice = item.PriceSnapshot;
                    cartItem.DiscountAmount = item.DiscountAmount;
                    cartItem.TaxAmount = item.TaxAmount;
                    cartItem.TotalPrice = item.PriceSnapshot;                    
                    cartItem.ProductOptions = item.SelectedOptionsJson;                    
                    cartItem.Sku = item.SkuSnapshot;
                    cartItem.ProductName = item.ItemNameSnapshot;
                    cartItem.ProductType = item.ProviderType;
                    cartItems.Add(cartItem);
                }

                var cartDto = new OrderCartDto
                {
                    UserId = cart.UserId,
                    SubTotal = cart.Subtotal,
                    TotalAmount = cart.TotalAmount,
                    DiscountAmount = cart.TotalDiscount,
                    PlatformFee = cart.PlatformFee,
                    SurgeFee = cart.SurgeFee,
                    TaxAmount = cart.TotalTax,
                    CartItem = cartItems
                };

                return Result.Success(cartDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOrderCartByUserIdQuery: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("Error", "Failed to get cart"));
            }
        }
    }
}
