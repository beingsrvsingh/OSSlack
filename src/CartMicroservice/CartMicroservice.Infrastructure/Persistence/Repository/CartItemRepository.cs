using CartMicroservice.Domain.Core.Repositories;
using CartMicroservice.Domain.Entities;
using CartMicroservice.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace CartMicroservice.Infrastructure.Persistence.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly CartDbContext _context;

        public CartItemRepository(CartDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<CartItem?> GetItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && !ci.IsDeleted);
        }

        public async Task<IEnumerable<CartItem>> GetItemsByCartIdAsync(int cartId)
        {
            return await _context.CartItems
                .Where(ci => ci.CartId == cartId && !ci.IsDeleted)
                .ToListAsync();
        }

    }
}