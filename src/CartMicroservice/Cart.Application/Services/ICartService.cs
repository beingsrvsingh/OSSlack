using CartMicroservice.Application.Contracts;
using CartMicroservice.Domain.Entities;

namespace CartMicroservice.Application.Services
{
    public interface ICartService
    {
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task<Cart?> GetCartWithItemsAsync(int cartId);

        Task<bool> AddCartItemAsync(Cart item);
        Task<bool> UpdateCartItemAsync(Cart item);
        Task<bool> RemoveCartItemAsync(int cartItemId);
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId);
        Task<bool> ClearCartItemsAsync(int cartId);
    }

}