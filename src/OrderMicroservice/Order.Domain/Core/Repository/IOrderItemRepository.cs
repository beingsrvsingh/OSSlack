using Order.Domain.Entities;
using Shared.Domain.Repository;

namespace Order.Domain.Core.Repository
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<List<OrderItem>> GetTrendingProductsByCategoryAsync(DateTime fromDate);
    }
}