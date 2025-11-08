using PriestMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Priest.Domain.Entities
{
    public class PriestExpertiseMedia : BaseMedia
    {
        [Required]
        public int PriestExpertiseId { get; set; }

        [ForeignKey(nameof(PriestExpertiseId))]
        public virtual PriestExpertise PriestExpertise { get; set; } = null!;
    }
}
