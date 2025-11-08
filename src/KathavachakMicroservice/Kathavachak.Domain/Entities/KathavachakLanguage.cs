using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakLanguage
    {
        [Key]
        public int Id { get; set; }

        public int KathavachakId { get; set; }
        public int LanguageId { get; set; }
        public string? LanguageName { get; set; }

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
