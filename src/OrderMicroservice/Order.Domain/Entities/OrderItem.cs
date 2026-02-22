using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain;

namespace Order.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }  // FK to OrderHeader

        [Required]
        public int ProductVariantId { get; set; }  // Reference to product microservice

        public int? BookingId { get; set; }  // Reference to booking microservice

        [Required]
        [MaxLength(20)]
        public ProviderType ProductType { get; set; } // Product, Service, Kit
        
        [MaxLength(300)]
        public string? ProductUrl { get; set; }

        [Required, MaxLength(150)]
        public string ProductName { get; set; } = null!;

        [MaxLength(100)]
        public string? Sku { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }  // Price per unit before tax/discount

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }  // Total tax for this item (all units)

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } = 0m;  // Total discount on this item

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }  // (Quantity * UnitPrice) + Tax - Discount

        [Column(TypeName = "decimal(18,4)")]
        public decimal? WeightValue { get; set; }  // Numeric weight for shipping calculations

        [MaxLength(20)]
        public string? WeightUnit { get; set; }  // e.g. "kg", "lb"

        [MaxLength(1000)]
        public string? ProductOptions { get; set; }  // JSON string for custom options, add-ons

        [MaxLength(50)]
        public string? FulfillmentStatus { get; set; } = "Pending";  // e.g. Pending, Shipped, Returned

        [MaxLength(50)]
        public string? ReturnStatus { get; set; } = "None";  // e.g. None, Requested, Completed

        [MaxLength(1000)]
        public string? CustomerNotes { get; set; }  // Special requests or notes per item

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        // Navigation property
        public virtual OrderHeader OrderHeader { get; set; } = null!;
        public virtual ICollection<OrderItemCustomization> Customizations { get; set; } = new List<OrderItemCustomization>();
    }
}