
namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerConsultationMode
    {
        public int Id { get; set; }
        public int AstrologerId { get; set; }
        public virtual Astrologer Astrologer { get; set; } = null!;
        public int ConsultationModeMasterId { get; set; }
        public virtual ConsultationModeMaster ConsultationModeMaster { get; set; } = null!;
    }
}