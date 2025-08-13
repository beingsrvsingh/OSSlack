using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Priest.Domain.Entities.Enums;

namespace Priest.Domain.Entities
{
    public class ConsultationMode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PriestId { get; set; }

        [Required, MaxLength(100)]
        public ConsultationModeType ConsultationModeType { get; set; } = ConsultationModeType.None;

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster Priest { get; set; } = null!;
    }
}