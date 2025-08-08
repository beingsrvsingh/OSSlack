using CartMicroservice.Application.Services;
using CartMicroservice.Domain.Core.Repositories;
using CartMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace CartMicroservice.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILoggerService<CartService> _logger;

        public CartService(ICartRepository cartRepository, ILoggerService<CartService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
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

        public async Task<Cart?> GetCartWithItemsAsync(int cartId)
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

        public async Task<bool> AddOrUpdateCartItemAsync(CartItem item)
        {
            try
            {
                await _cartRepository.AddOrUpdateCartItemAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddOrUpdateCartItemAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> RemoveCartItemAsync(int cartItemId)
        {
            try
            {
                return await _cartRepository.RemoveCartItemAsync(cartItemId);
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
    }

}