using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Order.Domain.Enums;
using Shared.Domain.Enums;

namespace Order.Domain.Entities
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string OrderNumber { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public required string UserId { get; set; }

        [Required]
        public required int AddressId { get; set; }

        public int? BookingId { get; set; }

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
    }
}