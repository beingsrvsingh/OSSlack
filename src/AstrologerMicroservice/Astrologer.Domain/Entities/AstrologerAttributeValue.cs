using Shared.Domain.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerAttributeValue : IBaseAttributeValue
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

        public int? AstrologerId { get; set; }
        public int? ExpertiseId { get; set; }

        [ForeignKey(nameof(AstrologerId))]
        public virtual AstrologerMaster? AstrologerMaster { get; set; } = null!;

        [ForeignKey(nameof(ExpertiseId))]
        public virtual AstrologerExpertise? AstrologerExpertise { get; set; }
    }

}
