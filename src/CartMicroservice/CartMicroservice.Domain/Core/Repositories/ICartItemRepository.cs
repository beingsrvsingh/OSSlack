using CartMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace CartMicroservice.Domain.Core.Repositories
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetItemsByCartIdAsync(int cartId);
        Task<CartItem?> GetItemByIdAsync(int cartItemId);
    }

}