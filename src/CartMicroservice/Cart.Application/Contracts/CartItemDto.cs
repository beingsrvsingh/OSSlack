
namespace CartMicroservice.Application.Contracts
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductType { get; set; } = null!;
        public string ItemNameSnapshot { get; set; } = null!;
        public decimal PriceSnapshot { get; set; }
        public int Quantity { get; set; }
    }

}