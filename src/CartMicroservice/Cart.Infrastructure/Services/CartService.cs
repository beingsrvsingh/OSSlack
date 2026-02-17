using Azure.Core;
using CartMicroservice.Application.Services;
using CartMicroservice.Domain.Core.Repositories;
using CartMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace CartMicroservice.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly ILoggerService<CartService> _logger;

        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository, ILoggerService<CartService> logger)
        {
            _cartRepository = cartRepository;
            this.cartItemRepository = cartItemRepository;
            _logger = logger;
        }

        public async Task<CartMicroservice.Domain.Entities.Cart?> GetCartByUserIdAsync(string userId)
        {
            try
            {
                return await _cartRepository.GetCartByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCartByUserIdAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<CartMicroservice.Domain.Entities.Cart?> GetCartByProductIdAsync(int productId)
        {
            try
            {
                return await _cartRepository.GetCartByProductIdAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCartByProductIdAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<CartMicroservice.Domain.Entities.Cart?> GetCartWithItemsAsync(int cartId)
        {
            try
            {
                return await _cartRepository.GetCartWithItemsAsync(cartId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCartWithItemsAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<bool> AddCartAsync(CartMicroservice.Domain.Entities.Cart item)
        {
            try
            {
                await _cartRepository.AddCartItemAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddCartItemAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> AddCartItemAsync(CartMicroservice.Domain.Entities.CartItem item)
        {
            try
            {
                await cartItemRepository.AddCartItemAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddCartItemAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateCartItemQuantityAsync(CartMicroservice.Domain.Entities.Cart cart, int productVariantId, int quantity)
        {
            try
            {
                cart.UpdateItemQuantity(productVariantId, quantity);
                await _cartRepository.UpdateCartItemQuantityAsync(cart);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateCartItemQuantityAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateCartAsync(CartMicroservice.Domain.Entities.Cart cart)
        {
            try
            {
                await _cartRepository.UpdateCartIAsync(cart);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateCartItemQuantityAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> RemoveCartAsync(int cartId)
        {
            try
            {
                return await _cartRepository.RemoveCartAsync(cartId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in RemoveCartItemAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> RemoveCartItemAsync(int productId)
        {
            try
            {
                return await cartItemRepository.RemoveCartItemAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in RemoveCartItemAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId)
        {
            try
            {
                return await _cartRepository.GetCartItemsAsync(cartId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCartItemsAsync: {ex.Message}", ex);
                return Enumerable.Empty<CartItem>();
            }
        }

        public async Task<bool> ClearCartItemsAsync(int cartId)
        {
            try
            {
                await _cartRepository.ClearCartItemsAsync(cartId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ClearCartItemsAsync: {ex.Message}", ex);
                return false;
            }
        }

        public void RecalculateCart(CartMicroservice.Domain.Entities.Cart cart)
        {
            cart.Subtotal = cart.CartItems.Sum(x => x.PriceSnapshot);
            cart.TotalDiscount = cart.CartItems.Sum(x => x.DiscountAmount);
            cart.TotalTax = cart.CartItems.Sum(x => x.TaxAmount);
            cart.TotalAmount = cart.Subtotal - cart.TotalDiscount + cart.TotalTax;
        }
    }

}