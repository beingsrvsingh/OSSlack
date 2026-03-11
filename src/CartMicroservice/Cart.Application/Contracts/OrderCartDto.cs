namespace Cart.Application.Contracts
{
    public class OrderCartDto
    {
        public required string UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal SurgeFee { get; set; }

        public decimal PlatformFee { get; set; }

        public decimal SubTotal { get; set; }

        public List<OrderCartItemDto> CartItem { get; set; } = default!;
    }
}
