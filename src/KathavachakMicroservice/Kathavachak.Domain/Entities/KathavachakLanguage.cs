using System.ComponentModel.DataAnnotations;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakLanguage
    {
        [Key]
        public int Id { get; set; }

        public int KathavachakId { get; set; }

        [Required, MaxLength(10)]
        public string LanguageCode { get; set; } = null!;

        public KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
