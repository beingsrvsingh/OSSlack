using Order.Domain.Entities;

namespace Order.Application.Services
{
    public interface IOrderService
    {
        Task<OrderHeader?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
        Task<OrderShippingAddress?> GetShippingAddressAsync(int orderId);
        Task<IEnumerable<OrderItemCustomization>> GetCustomizationsByOrderItemIdAsync(int orderItemId);
        Task<bool> AddOrderAsync(OrderHeader order);
        Task<IEnumerable<OrderHeader>> GetAllOrdersAsync();
        Task<IEnumerable<OrderHeader>> GetOrdersByUserIdAsync(int customerId);
        Task<bool> UpdateOrderAsync(OrderHeader order);
        Task<bool> DeleteOrderAsync(int orderId);
    }

}