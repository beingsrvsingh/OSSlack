
namespace Order.Application.Contracts
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }

        public List<OrderItemCustomizationDto> Customizations { get; set; } = new();
    }
}