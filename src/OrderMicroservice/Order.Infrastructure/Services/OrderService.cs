using Order.Application.Services;
using Order.Domain.Core.Repository;
using Order.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Order.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILoggerService<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ILoggerService<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<OrderHeader?> GetOrderByIdAsync(int orderId)
        {
            try
            {
                return await _orderRepository.GetOrderByIdAsync(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching order by ID", ex);
                return null;
            }
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            try
            {
                return await _orderRepository.GetOrderItemsAsync(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching order items", ex);
                return Enumerable.Empty<OrderItem>();
            }
        }

        public async Task<OrderShippingAddress?> GetShippingAddressAsync(int orderId)
        {
            try
            {
                return await _orderRepository.GetShippingAddressAsync(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching shipping address", ex);
                return null;
            }
        }

        public async Task<IEnumerable<OrderItemCustomization>> GetCustomizationsByOrderItemIdAsync(int orderItemId)
        {
            try
            {
                return await _orderRepository.GetCustomizationsByOrderItemIdAsync(orderItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching order item customizations", ex);
                return Enumerable.Empty<OrderItemCustomization>();
            }
        }

        public async Task<bool> AddOrderAsync(OrderHeader order)
        {
            try
            {
                await _orderRepository.AddOrderAsync(order);
                await _orderRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding order", ex);
                return false;
            }
        }

        public async Task<IEnumerable<OrderHeader>> GetAllOrdersAsync()
        {
            try
            {
                return await _orderRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllOrdersAsync: {ex.Message}", ex);
                return Enumerable.Empty<OrderHeader>();
            }
        }

        public async Task<IEnumerable<OrderHeader>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                return await _orderRepository.GetOrdersByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOrdersByUserIdAsync: {ex.Message}", ex);
                return Enumerable.Empty<OrderHeader>();
            }
        }

        public async Task<bool> UpdateOrderAsync(OrderHeader order)
        {
            try
            {
                await _orderRepository.UpdateAsync(order);
                await _orderRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateOrderAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if (order == null) return false;

                await _orderRepository.DeleteAsync(order);
                await _orderRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteOrderAsync: {ex.Message}", ex);
                return false;
            }
        }
    }

}