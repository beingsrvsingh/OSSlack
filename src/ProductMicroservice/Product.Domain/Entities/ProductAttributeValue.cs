using Shared.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public partial class ProductAttributeValue : IBaseAttributeValue
    {
        [Key]
        public int Id { get; set; }

        public int? CatalogAttributeId { get; set; }
        public int? CatalogAttributeValueId { get; set; }
        public string? Value { get; set; }
        public string? AttributeKey { get; set; }
        public string? AttributeLabel { get; set; }
        public int? AttributeDataTypeId { get; set; }
        public int? CatalogAttributeGroupId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? ProductMasterId { get; set; }

        [ForeignKey(nameof(ProductMasterId))]
        public virtual ProductMaster ProductMaster { get; set; } = null!;

        public int? ProductVariantId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariantMaster? ProductVariant { get; set; }
    }

}