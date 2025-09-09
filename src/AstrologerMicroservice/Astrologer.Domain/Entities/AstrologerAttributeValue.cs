using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerAttributeValue : BaseAttributeValue
    {
        [Key]
        public int Id { get; set; }
        public int ExpertiseId { get; set; }

        [ForeignKey(nameof(ExpertiseId))]
        public virtual AstrologerExpertise AstrologerExpertise { get; set; } = null!;
    }

}
