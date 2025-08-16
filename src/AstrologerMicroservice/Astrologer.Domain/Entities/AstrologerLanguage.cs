
namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerLanguage
    {
        public int AstrologerId { get; set; }
        public int LanguageId { get; set; }
        public Astrologer Astrologer { get; set; } = null!;
        public LanguageMaster Language { get; set; } = null!;
    }

}