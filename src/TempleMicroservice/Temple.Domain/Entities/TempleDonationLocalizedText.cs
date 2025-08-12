using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TempleDonationLocalizedText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TempleDonationId { get; set; }

        [Required, MaxLength(5)]
        public string LanguageCode { get; set; } = "en";

        [Required, MaxLength(200)]
        public string LocalizedName { get; set; } = null!;

        [MaxLength(1000)]
        public string? LocalizedDescription { get; set; }

        [ForeignKey("TempleDonationId")]
        public TempleDonation TempleDonation { get; set; } = null!;
    }

}