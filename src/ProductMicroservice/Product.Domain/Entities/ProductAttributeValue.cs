using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductAttributeValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductMasterId { get; set; }

        [ForeignKey(nameof(ProductMasterId))]
        public virtual ProductMaster ProductMaster { get; set; } = null!;

        [Required]
        public int CatalogAttributeId { get; set; }

        // Optional: Denormalize attribute metadata to avoid extra API calls
        public string? AttributeKey { get; set; }  // e.g. "color"
        public string? AttributeLabel { get; set; } // e.g. "Color"
        public int? AttributeDataType { get; set; } // e.g. enum int value for AttributeDataType enum

        [Required]
        public string Value { get; set; } = null!; // Store value as string, interpret based on CatalogAttribute.DataType

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}