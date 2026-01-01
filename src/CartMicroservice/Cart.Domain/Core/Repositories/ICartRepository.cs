using CartMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace CartMicroservice.Domain.Core.Repositories
{
    public interface ICartRepository : IRepository<CartMicroservice.Domain.Entities.Cart>
    {
        // Fetch cart by UserId including items
        Task<CartMicroservice.Domain.Entities.Cart?> GetCartByUserIdAsync(string userId);

        // Fetch cart including items by CartId
        Task<CartMicroservice.Domain.Entities.Cart?> GetCartWithItemsAsync(int cartId);

        // Add or update a cart item
        Task AddCartItemAsync(CartMicroservice.Domain.Entities.Cart item);

        Task UpdateCartItemAsync(CartMicroservice.Domain.Entities.Cart item);

        // Remove cart item by id
        Task<bool> RemoveCartItemAsync(int cartItemId);

        // Get cart items for a given cart id
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId);

        // Optional: Clear cart items for a cart (e.g. on checkout)
        Task ClearCartItemsAsync(int cartId);
    }

}