using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class AttributeValue : BaseAttributeValue
    {
        [Key]
        public int Id { get; set; }
        public int ExpertiseId { get; set; }

        [ForeignKey(nameof(ExpertiseId))]
        public virtual TempleExpertise TempleExpertise { get; set; } = null!;
    }
}
