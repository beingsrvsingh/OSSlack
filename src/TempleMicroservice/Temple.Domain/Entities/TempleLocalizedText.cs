using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TempleLocalizedText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TempleId { get; set; }

        [Required, MaxLength(5)]
        public string LanguageCode { get; set; } = "en";

        [MaxLength(200)]
        public string? LocalizedName { get; set; }

        public string? LocalizedDescription { get; set; }

        [ForeignKey("TempleId")]
        public TempleMaster Temple { get; set; } = null!;
    }

}