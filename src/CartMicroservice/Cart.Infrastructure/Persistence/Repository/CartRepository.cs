using CartMicroservice.Domain.Core.Repositories;
using CartMicroservice.Domain.Entities;
using CartMicroservice.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace CartMicroservice.Infrastructure.Persistence.Repository
{
    public class CartRepository : Repository<CartMicroservice.Domain.Entities.Cart>, ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(CartDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<CartMicroservice.Domain.Entities.Cart?> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);
        }

        public async Task<CartMicroservice.Domain.Entities.Cart?> GetCartByProductIdAsync(int productId)
        {
            return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c =>c.CartItems.Any(ci => ci.ProductVariantId == productId));
        }

        public async Task<CartMicroservice.Domain.Entities.Cart?> GetCartWithItemsAsync(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cartId && !c.IsDeleted);
        }

        public async Task AddCartItemAsync(CartMicroservice.Domain.Entities.Cart item)
        {
            await _context.Carts.AddAsync(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartIAsync(CartMicroservice.Domain.Entities.Cart item)
        {
            _context.Carts.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(CartMicroservice.Domain.Entities.Cart item)
        {
            _context.Carts.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveCartAsync(int cartId)
        {
            var item = await _context.Carts.FindAsync(cartId);
            if (item == null) return false;

            _context.Carts.Remove(item);
            await _context.SaveChangesAsync();
            return true;
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