using Shared.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class AttributeValue : IBaseAttributeValue
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
        
        public int? TempleId { get; set; }

        [ForeignKey(nameof(TempleId))]
        public virtual TempleMaster? TempleMaster { get; set; } = null!;

        public int? ExpertiseId { get; set; }

        [ForeignKey(nameof(ExpertiseId))]
        public virtual TempleExpertise? TempleExpertise { get; set; } = null!;
    }
}
