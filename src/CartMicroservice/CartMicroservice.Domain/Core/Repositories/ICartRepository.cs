using CartMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace CartMicroservice.Domain.Core.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        // Fetch cart by UserId including items
        Task<Cart?> GetCartByUserIdAsync(string userId);

        // Fetch cart including items by CartId
        Task<Cart?> GetCartWithItemsAsync(int cartId);

        // Add or update a cart item
        Task AddOrUpdateCartItemAsync(CartItem item);

        // Remove cart item by id
        Task<bool> RemoveCartItemAsync(int cartItemId);

        // Get cart items for a given cart id
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId);

        // Optional: Clear cart items for a cart (e.g. on checkout)
        Task ClearCartItemsAsync(int cartId);
    }

}