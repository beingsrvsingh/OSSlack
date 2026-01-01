using CartMicroservice.Application.Features.Commands;
using CartMicroservice.Application.Services;
using CartMicroservice.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.EventHandlers.Commands
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, Result>
    {
        private readonly ICartService _cartService;
        private readonly ILoggerService<AddCartItemCommandHandler> _logger;

        public AddCartItemCommandHandler(ICartService cartService, ILoggerService<AddCartItemCommandHandler> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Cart
                {
                    UserId = request.CartItem.UserId,
                    TotalAmount = (decimal)request.CartItem.Amount,
                    TotalDiscount = (decimal)request.CartItem.Discount,
                    TotalTax = (decimal)request.CartItem.Tax,                    
                };

                var carts = new List<CartItem>();

                foreach (var cart in request.CartItem.CartItems)
                {
                    var CartItem = new CartItem
                    {
                        ProductVariantId = cart.ProductVariantId,
                        ProviderType = cart.ProductType,
                        ItemNameSnapshot = cart.ProductName,
                        Quantity = cart.Quantity,
                        PriceSnapshot = (decimal)cart.Amount,
                        DiscountAmount = (decimal)cart.Discount,
                        TaxAmount = (decimal)cart.Tax,
                    };

                    carts.Add(CartItem);

                }

                item.CartItems = carts;

                bool success = success = await _cartService.AddCartItemAsync(item);

                return success ? Result.Success() : Result.Failure(new FailureResponse("Error", "Failed to add cart item"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddCartItemCommandHandler: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("Error", "Exception occurred"));
            }
        }
    }
}