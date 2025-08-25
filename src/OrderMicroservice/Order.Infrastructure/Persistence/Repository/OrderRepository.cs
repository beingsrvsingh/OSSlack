using Microsoft.EntityFrameworkCore;
using Order.Domain.Core.Repository;
using Order.Domain.Entities;
using Order.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Order.Infrastructure.Persistence.Repository
{
    public class OrderRepository : Repository<OrderHeader>, IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<OrderHeader?> GetOrderByIdAsync(int orderId)
        {
            return await _context.OrderHeaders
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Customizations)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            return await _context.OrderItems
                .Where(i => i.OrderHeaderId == orderId)
                .Include(i => i.Customizations)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderItemCustomization>> GetCustomizationsByOrderItemIdAsync(int orderItemId)
        {
            return await _context.OrderItemCustomizations
                .Where(c => c.OrderItemId == orderItemId)
                .ToListAsync();
        }

        public async Task AddOrderAsync(OrderHeader order)
        {
            await _context.OrderHeaders.AddAsync(order);
        }

        public async Task<IEnumerable<OrderHeader>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.OrderHeaders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<OrderHeader?> GetOrderDetailsAsync(int orderId)
        {
            return await _context.OrderHeaders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

    }

}