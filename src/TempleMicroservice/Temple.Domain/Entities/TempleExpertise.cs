
namespace Temple.Domain.Entities
{
    public class TempleExpertise
    {
        public int AstrologerId { get; set; }
        public int ExpertiseId { get; set; }
        public TempleMaster TempleMaster { get; set; } = null!;
        public Expertise Expertise { get; set; } = null!;
    }

}