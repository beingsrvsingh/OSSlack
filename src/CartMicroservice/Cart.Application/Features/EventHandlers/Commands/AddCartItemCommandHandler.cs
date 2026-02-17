using CartMicroservice.Application.Features.Commands;
using CartMicroservice.Application.Services;
using CartMicroservice.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.EventHandlers.Commands
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, Result>
    {
        private readonly ICartService _cartService;
        private readonly ILoggerService<AddCartItemCommandHandler> _logger;
        private readonly IUserProvider userProvider;

        public AddCartItemCommandHandler(ICartService cartService, ILoggerService<AddCartItemCommandHandler> logger, IUserProvider userProvider)
        {
            _cartService = cartService;
            _logger = logger;
            this.userProvider = userProvider;
        }

        public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = userProvider.UserId ?? "Test-User";

                var cart = await _cartService.GetCartByUserIdAsync(userId);

                bool success = false;

                if (cart == null)
                {
                    cart = new CartMicroservice.Domain.Entities.Cart
                    {
                        UserId = userId,
                        TotalAmount = (decimal)request.CartItem.Amount,
                        Subtotal = (decimal)request.CartItem.Amount,
                        CartItems = new List<CartItem>()
                        {
                            new CartItem
                            {
                                ProductVariantId = request.CartItem.ProductVariantId,
                                ProviderType = request.CartItem.ProductType,
                                ItemNameSnapshot = request.CartItem.ProductName,
                                Quantity = request.CartItem.Quantity,
                                PriceSnapshot = request.CartItem.Quantity * (decimal)request.CartItem.Amount,
                                ImageUrl = request.CartItem.ImageUrl
                            }
                        }
                    };

                    success = await _cartService.AddCartAsync(cart);
                }
                else if (cart.CartItems.Any((c) => c.ProductVariantId == request.CartItem.ProductVariantId && c.CartId == cart.Id))
                {
                    return Result.Success(cart.Id);
                }
                else
                {
                    cart.TotalAmount = (decimal)request.CartItem.Amount + cart.TotalAmount;
                    cart.Subtotal = (decimal)request.CartItem.Amount + cart.Subtotal;

                    var cartItem = new CartItem
                    {
                        CartId = cart.Id,
                        ProductVariantId = request.CartItem.ProductVariantId,
                        ProviderType = request.CartItem.ProductType,
                        ItemNameSnapshot = request.CartItem.ProductName,
                        Quantity = request.CartItem.Quantity,
                        PriceSnapshot = request.CartItem.Quantity * (decimal)request.CartItem.Amount,
                        ImageUrl = request.CartItem.ImageUrl
                    };

                    success = await _cartService.AddCartItemAsync(cartItem);

                    if (success) {
                        success = await _cartService.UpdateCartAsync(cart);
                    }
                }                

                return success
                    ? Result.Success(cart.Id)
                    : Result.Failure(new FailureResponse("Error", "Failed to add cart item"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddCartItemCommandHandler");
                return Result.Failure(new FailureResponse("Error", "Exception occurred"));
            }
        }
    }
}