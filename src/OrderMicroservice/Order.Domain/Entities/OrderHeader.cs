using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Order.Domain.Enums;

namespace Order.Domain.Entities
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string OrderNumber { get; set; } = null!;  // Unique, human-friendly order reference

        [Required]
        public int CustomerId { get; set; }  // Reference to customer (could be user service)

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }

        [MaxLength(100)]
        public string? ShippingMethod { get; set; }
        [MaxLength(100)]
        public string? TrackingNumber { get; set; }

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

        [MaxLength(3)]
        public string CurrencyCode { get; set; } = "USD";

        public bool IsGift { get; set; } = false;

        [MaxLength(500)]
        public string? GiftMessage { get; set; }

        [MaxLength(1000)]
        public string? CustomerNotes { get; set; }

        [MaxLength(1000)]
        public string? AdminNotes { get; set; }

        [MaxLength(50)]
        public string? RefundStatus { get; set; }  // e.g., "None", "Requested", "Completed"

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<OrderShippingAddress> ShippingAddresses { get; set; } = new List<OrderShippingAddress>();
    }
}