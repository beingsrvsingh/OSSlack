
using Order.Domain.Entities;
using Order.Domain.Enums;
using Shared.Utilities.Cryptography;

namespace Order.Application.Contracts
{
    public class OrderDto
    {
        public string OrderNumber { get; set; } = null!;
        public string OrderItemId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public double Cost { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; } = OrderStatus.Pending.ToString();

        public static List<OrderDto> ToResponseDtoList(IEnumerable<OrderHeader> orderHeaders)
        {
            return orderHeaders
                .SelectMany(order => order.OrderItems.Select(item => new OrderDto
                {
                    OrderNumber = item.OrderHeader.OrderNumber,
                    OrderItemId = Cryptography.EncryptString(item.Id.ToString()),
                    Title = item.ProductName,
                    Url = item.ProductUrl ?? string.Empty,
                    Cost = (double)item.TotalPrice,
                    DeliveryDate = order.EstimatedDeliveryDate ?? DateTime.UtcNow,
                    Status = order.Status.ToString()
                }))
                .ToList();
        }

    }

}