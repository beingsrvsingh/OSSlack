using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TempleExpertiseImage : BaseMedia
    {
        [Required]
        public int TempleExpertiseId { get; set; }

        [ForeignKey(nameof(TempleExpertiseId))]
        public virtual TempleExpertise TempleExpertise { get; set; } = null!;
    }
}
