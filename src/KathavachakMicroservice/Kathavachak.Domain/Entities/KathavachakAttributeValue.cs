using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakAttributeValue : IBaseAttributeValue
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
        public string? CategoryNameSnapshot { get; set; }

        public int ExpertiseId { get; set; }

        [ForeignKey(nameof(ExpertiseId))]
        public virtual KathavachakExpertise Expertise { get; set; } = null!;
    }

}
