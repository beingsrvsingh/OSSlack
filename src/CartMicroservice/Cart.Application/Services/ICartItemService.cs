using CartMicroservice.Domain.Entities;

namespace CartMicroservice.Application.Services
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItem>> GetItemsByCartIdAsync(int cartId);
        Task<CartItem?> GetItemByIdAsync(int cartItemId);
    }

}