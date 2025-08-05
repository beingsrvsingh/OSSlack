
namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerExpertise
    {
        public int AstrologerId { get; set; }
        public int ExpertiseId { get; set; }
        public Astrologer Astrologer { get; set; } = null!;
        public Expertise Expertise { get; set; } = null!;
    }

}