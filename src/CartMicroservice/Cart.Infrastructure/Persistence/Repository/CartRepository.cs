using CartMicroservice.Domain.Core.Repositories;
using CartMicroservice.Domain.Entities;
using CartMicroservice.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace CartMicroservice.Infrastructure.Persistence.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(CartDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);
        }

        public async Task<Cart?> GetCartWithItemsAsync(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cartId && !c.IsDeleted);
        }

        public async Task AddOrUpdateCartItemAsync(CartItem item)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == item.CartId
                                        && ci.ProductId == item.ProductId
                                        && ci.ProviderType == item.ProviderType
                                        && !ci.IsDeleted);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                _context.CartItems.Update(existingItem);
            }
            else
            {
                await _context.CartItems.AddAsync(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveCartItemAsync(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null) return false;

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId)
        {
            return await _context.CartItems
                .Where(ci => ci.CartId == cartId && !ci.IsDeleted)
                .ToListAsync();
        }

        public async Task ClearCartItemsAsync(int cartId)
        {
            var items = await _context.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }

}