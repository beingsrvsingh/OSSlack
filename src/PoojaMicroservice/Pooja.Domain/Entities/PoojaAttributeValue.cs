using Shared.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pooja.Domain.Entities
{
    public partial class PoojaAttributeValue : IBaseAttributeValue
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

        public int? PoojaMasterId { get; set; }

        [ForeignKey(nameof(PoojaMasterId))]
        public virtual PoojaMaster PoojaMaster { get; set; } = null!;

        public int? PoojaVariantId { get; set; }

        [ForeignKey(nameof(PoojaVariantId))]
        public virtual PoojaVariantMaster? PoojaVariantMaster { get; set; }
        public string? AttributeGroupNameSnapshot { get; set; }
    }
}
