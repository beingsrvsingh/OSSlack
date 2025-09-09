using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Priest.Domain.Entities.Enums;

namespace PriestMicroservice.Domain.Entities
{
    public class ConsultationMode
    {
        [Key]
        public int Id { get; set; }

        public int ExpertiseId { get; set; }

        public int ConsultationModeMasterId { get; set; }
        public ConsultationModeType Mode { get; set; }

        [ForeignKey(nameof(ExpertiseId))]
        public PriestExpertise Expertise { get; set; } = null!;

        [ForeignKey(nameof(ConsultationModeMasterId))]
        public virtual ConsultationModeMaster ConsultationModeMaster { get; set; } = null!;
    }
}