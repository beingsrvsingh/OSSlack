using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakExpertiseMedia : BaseMedia
    {
        [Required]
        public int KathavachakExpertiseId { get; set; }

        [ForeignKey(nameof(KathavachakExpertiseId))]
        public virtual KathavachakExpertise KathavachakExpertise { get; set; } = null!;
    }
}
