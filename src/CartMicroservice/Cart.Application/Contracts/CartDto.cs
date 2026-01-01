
namespace CartMicroservice.Application.Contracts
{
    public class CartDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? RegionCode { get; set; }
        public string CurrencyCode { get; set; } = "INR";
        public decimal Subtotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalAmount { get; set; }
        public string? AppliedCouponCode { get; set; }
        public ICollection<AddCartDto> CartItems { get; set; } = new List<AddCartDto>();
    }

}