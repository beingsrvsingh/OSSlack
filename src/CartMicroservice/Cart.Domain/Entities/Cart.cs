using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartMicroservice.Domain.Entities
{
    public class Cart
    {
        [Key]
        [Column(name:"id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserId { get; set; } = null!;

        [MaxLength(50)]
        public string? RegionCode { get; set; }

        [MaxLength(10)]
        public string CurrencyCode { get; set; } = "INR";

        [MaxLength(50)]
        public string? TenantId { get; set; }

        public decimal Subtotal { get; set; } = 0m;
        public decimal TotalDiscount { get; set; } = 0m;
        public decimal TotalTax { get; set; } = 0m;
        public decimal TotalAmount { get; set; } = 0m;

        [MaxLength(50)]
        public string? AppliedCouponCode { get; set; }

        public decimal CouponDiscountAmount { get; set; } = 0m;

        public decimal PlatformFee { get; set; } = 0m;
        public decimal SurgeFee { get; set; } = 0m;

        public bool IsDeleted { get; set; } = false; // is_deleted

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }


        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Abandoned, CheckedOut

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public void UpdateItem(int productVariantId, decimal basePrice, int quantity)
        {
            var cartItem = CartItems
                .FirstOrDefault(ci => ci.ProductVariantId == productVariantId);

            if (cartItem == null)
                throw new Exception("Cart item not found.");

            cartItem.UpdateQuantity(basePrice, quantity);

            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
            Subtotal = CartItems.Sum(ci => ci.PriceSnapshot);
            TotalAmount = Subtotal - TotalDiscount + TotalTax;
        }

    }
}