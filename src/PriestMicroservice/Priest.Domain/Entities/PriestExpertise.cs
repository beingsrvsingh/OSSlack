using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Priest.Domain.enums;

namespace Priest.Domain.Entities
{
    public class PriestExpertise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PriestId { get; set; }

        [Required, MaxLength(100)]
        public PriestExpertiseType ExpertiseArea { get; set; } = PriestExpertiseType.None;

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster Priest { get; set; } = null!;
    }

}