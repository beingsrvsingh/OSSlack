
using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerConsultationMode
    {
        public int Id { get; set; }

        public int ExpertiseId { get; set; }

        public int ConsultationModeMasterId { get; set; }
        public string? ConsultationMode { get; set; }

        [ForeignKey(nameof(ExpertiseId))]
        public AstrologerExpertise Expertise { get; set; } = null!;

        [ForeignKey(nameof(ConsultationModeMasterId))]
        public virtual ConsultationModeMaster ConsultationModeMaster { get; set; } = null!;
    }
}