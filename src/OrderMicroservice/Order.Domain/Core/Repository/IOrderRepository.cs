using Order.Domain.Entities;
using Shared.Domain.Repository;

namespace Order.Domain.Core.Repository
{
    public interface IOrderRepository : IRepository<OrderHeader>
    {
        Task<OrderHeader?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
        Task<OrderShippingAddress?> GetShippingAddressAsync(int orderId);
        Task<IEnumerable<OrderItemCustomization>> GetCustomizationsByOrderItemIdAsync(int orderItemId);
        Task<IEnumerable<OrderHeader>> GetOrdersByUserIdAsync(int userId);
        Task AddOrderAsync(OrderHeader order);
    }
}