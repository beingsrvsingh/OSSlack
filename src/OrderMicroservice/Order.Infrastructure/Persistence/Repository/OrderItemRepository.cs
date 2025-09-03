using Microsoft.EntityFrameworkCore;
using Order.Domain.Core.Repository;
using Order.Domain.Entities;
using Order.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Order.Infrastructure.Persistence.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly OrderDbContext _context;

        public OrderItemRepository(OrderDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<List<OrderItem>> GetTrendingProductsByCategoryAsync(DateTime fromDate)
        {
            return await _context.OrderItems
                .Include(oi => oi.OrderHeader)
                .Where(oi => oi.OrderHeader.CreatedAt >= fromDate)
                .ToListAsync();
        }
    }
}