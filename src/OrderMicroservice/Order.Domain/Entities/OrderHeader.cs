using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Order.Domain.Enums;
using Shared.Domain.Enums;

namespace Order.Domain.Entities
{
    public class OrderHeader
    {
        public OrderHeader()
        {
            OrderNumber = GenerateTraceableOrderNumber();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string OrderNumber { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public required int AddressId { get; set; }

        public string? BookingId { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal ShippingFee { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal PlatformFee { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal SurgeFee { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal GrandTotal { get; set; }

        public bool IsGift { get; set; } = false;

        [MaxLength(500)]
        public string? GiftMessage { get; set; }

        [MaxLength(1000)]
        public string? CustomerNotes { get; set; }

        [MaxLength(1000)]
        public string? AdminNotes { get; set; }

        [MaxLength(50)]
        public string? RefundStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        // Navigation
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<OrderShipment> Shipments { get; set; } = new List<OrderShipment>();

        public string GenerateTraceableOrderNumber()
        {
            Random random = new Random();

            // Part 1: Date in yyMMdd format (e.g., 260312 for Mar 12, 2026)
            string datePart = DateTime.UtcNow.ToString("yyMMdd");

            // Part 2: Random 5-digit number
            string randomPart1 = random.Next(10000, 99999).ToString();

            // Part 3: Random 5-digit number
            string randomPart2 = random.Next(10000, 99999).ToString();

            // Combine like Amazon-style: DATE-RANDOM-RANDOM
            return $"{datePart}-{randomPart1}-{randomPart2}";
        }
    }
}