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
        public int OrderHeaderId { get; set; }
        public virtual OrderHeader OrderHeader { get; set; } = null!;

        [Required]
        public int ProductVariantId { get; set; }

        [MaxLength(50)]
        public string? ProductType { get; set; }

        [MaxLength(255)]
        public string? ProductUrl { get; set; }

        [Required, MaxLength(255)]
        public string ProductName { get; set; } = null!;

        [MaxLength(100)]
        public string? SKU { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public decimal? WeightValue { get; set; }
        [MaxLength(20)]
        public string? WeightUnit { get; set; }

        // JSON string
        public string? ProductOptions { get; set; }

        [MaxLength(50)]
        public string FulfillmentStatus { get; set; } = "Pending";

        [MaxLength(50)]
        public string ReturnStatus { get; set; } = "None";

        [MaxLength(1000)]
        public string? CustomerNotes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }
        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        // Navigation
        public virtual ICollection<OrderItemCustomization> OrderItemCustomizations { get; set; } = new List<OrderItemCustomization>();
        public virtual ICollection<OrderShipmentItem> ShipmentItems { get; set; } = new List<OrderShipmentItem>();
    }
}