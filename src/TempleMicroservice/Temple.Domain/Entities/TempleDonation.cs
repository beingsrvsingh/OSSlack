using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public partial class TempleDonation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TempleId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(100)]
        public string DonorName { get; set; } = null!;

        [MaxLength(500)]
        public string? Message { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;
        public ICollection<TempleDonationLocalizedText> Localizations { get; set; } = new List<TempleDonationLocalizedText>();

    }

}