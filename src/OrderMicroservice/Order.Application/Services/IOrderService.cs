using Order.Application.Contracts;
using Order.Application.Features.Commands;
using Order.Domain.Entities;

namespace Order.Application.Services
{
    public interface IOrderService
    {
        Task<OrderHeader?> GetOrderByIdAsync(int orderId);
        Task<OrderDetailDto?> GetOrderDetailsAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
        Task<IEnumerable<OrderItemCustomization>> GetCustomizationsByOrderItemIdAsync(int orderItemId);
        Task<OrderResponse?> AddOrderAsync(AddOrderCommand order);
        Task<IEnumerable<OrderHeader>> GetAllOrdersAsync();
        Task<IEnumerable<OrderHeader>> GetOrdersByUserIdAsync(string userId);
        Task<bool> UpdateOrderAsync(OrderHeader order);
        Task<bool> DeleteOrderAsync(int orderId);

        Task<OrderHeader?> GetOrderByOrderNumberAsync(string orderNumber);
    }

}