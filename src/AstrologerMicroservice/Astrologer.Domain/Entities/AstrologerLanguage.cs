
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerLanguage
    {
        [Key]
        public int Id { get; set; }
        public int AstrologerId { get; set; }
        public int LanguageId { get; set; }

        public string? LanguageName { get; set; }

        [ForeignKey(nameof(AstrologerId))]
        public AstrologerEntity Astrologer { get; set; } = null!;

        [ForeignKey(nameof(LanguageId))]
        public virtual LanguageMaster Language { get; set; } = null!;
    }

}