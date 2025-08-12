
using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public partial class TempleAarti
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TempleId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<TempleAartiLocalizedText> Localizations { get; set; } = new List<TempleAartiLocalizedText>();

    }

}