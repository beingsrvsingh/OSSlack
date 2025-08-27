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

        [Required]
        public string Value { get; set; } = null!;

        // Optional denormalized metadata
        public string? AttributeKey { get; set; }
        public string? AttributeLabel { get; set; }
        public int? AttributeDataTypeId { get; set; }

        // For sorting/grouping UI
        public int? CatalogAttributeGroupId { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}