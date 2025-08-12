using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TemplePoojaLocalizedText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TemplePoojaId { get; set; }

        [Required, MaxLength(5)]
        public string LanguageCode { get; set; } = "en";

        [Required, MaxLength(200)]
        public string LocalizedName { get; set; } = null!;

        [MaxLength(1000)]
        public string? LocalizedDescription { get; set; }

        [ForeignKey("TemplePoojaId")]
        public TemplePooja TemplePooja { get; set; } = null!;
    }

}