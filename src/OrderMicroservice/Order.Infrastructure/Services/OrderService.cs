using System.Security.Cryptography;
using Order.Application.Contracts;
using Order.Application.Services;
using Order.Domain.Core.Repository;
using Order.Domain.Entities;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Cryptography;

namespace Order.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILoggerService<OrderService> _logger;
        private readonly IPaymentService paymentService;
        private readonly IAddressService addressService;

        public OrderService(IOrderRepository orderRepository, ILoggerService<OrderService> logger,
        IPaymentService paymentService, IAddressService addressService)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            this.paymentService = paymentService;
            this.addressService = addressService;
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

        public async Task<OrderDetailDto?> GetOrderDetailsAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderDetailsAsync(orderId);
            if(order == null) return null;
            var payment = await paymentService.GetPaymentInfoByIdAsync(orderId);
            var shippingInfo = await addressService.GetAddressInfoByIdAsync(order.AddressId);

            if (order == null)
                return null;

            var dto = new OrderDetailDto
            {
                OrderSummaryDto = new OrderSummaryDto
                {
                    Name = order.OrderItems.FirstOrDefault()?.ProductName ?? "Unknown",
                    OrderDate = order.OrderDate,
                    OrderTotal = order.TotalAmount,
                    ConvenienceFee = 0,
                    Url = order.OrderItems.FirstOrDefault()?.ProductUrl ?? string.Empty
                },
                PaymentInfo = new PaymentInfoDto
                {
                    Mode = payment!.Mode, 
                    CardType = payment.CardType,
                    Name = payment.Name,
                    Status = payment.Status,
                    CardNumber = payment.CardNumber
                },
                BillDetails = new BillDetailsDto
                {
                    Cost = order.OrderItems.Sum(i => i.UnitPrice * i.Quantity),
                    Discount = order.DiscountAmount,
                    ItemTotal = order.TotalAmount - order.ShippingFee, // Simplified example
                    HandlingCharge = 0, // If you track handling charges, add here
                    DeliveryCharge = order.ShippingFee,
                    Total = order.TotalAmount
                },
                ShippingInfoDto = new ShippingInfoDto
                {
                    RecipientName = shippingInfo?.RecipientName ?? "",
                    AddressLine1 = shippingInfo?.AddressLine1 ?? "",
                    AddressLine2 = shippingInfo?.AddressLine2 ?? "",
                    City = shippingInfo?.City ?? "",
                    State = shippingInfo?.State ?? "",
                    PostalCode = shippingInfo?.PostalCode,
                    Country = shippingInfo?.Country ?? "",
                    PhoneNumber = shippingInfo?.PhoneNumber ?? "",
                    Email = shippingInfo?.Email ?? ""
                }
            };

            return dto;
        }

    }

}