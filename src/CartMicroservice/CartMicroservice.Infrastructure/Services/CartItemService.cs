using CartMicroservice.Application.Services;
using CartMicroservice.Domain.Core.Repositories;
using CartMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace CartMicroservice.Infrastructure.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ILoggerService<CartItemService> _logger;

        public CartItemService(ICartItemRepository cartItemRepository, ILoggerService<CartItemService> logger)
        {
            _cartItemRepository = cartItemRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CartItem>> GetItemsByCartIdAsync(int cartId)
        {
            try
            {
                return await _cartItemRepository.GetItemsByCartIdAsync(cartId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetItemsByCartIdAsync: {ex.Message}", ex);
                return Enumerable.Empty<CartItem>();
            }
        }

        public async Task<CartItem?> GetItemByIdAsync(int cartItemId)
        {
            try
            {
                return await _cartItemRepository.GetItemByIdAsync(cartItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetItemByIdAsync: {ex.Message}", ex);
                return null;
            }
        }
    }

}