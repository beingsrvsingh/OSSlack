using CartMicroservice.Application.Contracts;
using CartMicroservice.Domain.Entities;

namespace CartMicroservice.Application.Services
{
    public interface ICartService
    {
        Task<CartMicroservice.Domain.Entities.Cart?> GetCartByUserIdAsync(string userId);
        Task<CartMicroservice.Domain.Entities.Cart?> GetCartWithItemsAsync(int cartId);

        Task<bool> AddCartAsync(CartMicroservice.Domain.Entities.Cart item);
        Task<bool> AddCartItemAsync(CartMicroservice.Domain.Entities.CartItem item);
        Task<bool> RemoveCartItemAsync(int productId);

        Task<bool> RemoveCartAsync(int cartId);
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId);
        Task<bool> ClearCartItemsAsync(int cartId);

        Task<CartMicroservice.Domain.Entities.Cart?> GetCartByProductIdAsync(int productId);

        void RecalculateCart(CartMicroservice.Domain.Entities.Cart cart);

        Task<bool> UpdateCartItemAsync(CartMicroservice.Domain.Entities.Cart cart, int productVariantId, int quantity);
        Task<bool> UpdateCartAsync(CartMicroservice.Domain.Entities.Cart cart);
    }

}