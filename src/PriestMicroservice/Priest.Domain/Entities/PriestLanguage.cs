using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public class PriestLanguage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PriestId { get; set; }

        public int LanguageId { get; set; }

        public string? LanguageName { get; set; }

        [ForeignKey(nameof(PriestId))]
        public PriestMaster Priest { get; set; } = null!;

        [ForeignKey(nameof(LanguageId))]
        public virtual LanguageMaster Language { get; set; } = null!;
    }

}