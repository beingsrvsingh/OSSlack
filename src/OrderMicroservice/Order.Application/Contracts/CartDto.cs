using System.Text.Json.Serialization;

namespace Order.Application.Contracts
{
    public class CartDto
    {
        public required string UserId { get; set; }

        public DateTime PreferedDeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal SurgeFee { get; set; }

        public decimal PlatformFee { get; set; }

        public decimal GrandTotal { get; set; }

        public List<CartItemDto> CartItem { get; set; } = default!;
    }
}
