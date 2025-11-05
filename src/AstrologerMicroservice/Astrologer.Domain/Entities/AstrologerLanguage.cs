
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerLanguage
    {
        [Key]
        public int Id { get; set; }
        public int AstrologerId { get; set; }

        public string? LanguageName { get; set; }

        [ForeignKey(nameof(AstrologerId))]
        public AstrologerMaster Astrologer { get; set; } = null!;
    }

}