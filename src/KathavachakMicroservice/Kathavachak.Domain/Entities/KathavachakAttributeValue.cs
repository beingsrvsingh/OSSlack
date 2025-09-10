using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakAttributeValue : BaseAttributeValue
    {
        [Key]
        public int Id { get; set; }
        public int ExpertiseId { get; set; }

        [ForeignKey(nameof(ExpertiseId))]
        public virtual KathavachakExpertise Expertise { get; set; } = null!;
    }

}
