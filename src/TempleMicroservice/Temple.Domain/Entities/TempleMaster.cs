using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public partial class TempleMaster
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(300)]
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties if needed
        public virtual ICollection<TemplePooja> TemplePoojas { get; set; } = new List<TemplePooja>();
        public virtual ICollection<TempleDonation> Donations { get; set; } = new List<TempleDonation>();
        public virtual ICollection<TemplePrasad> Prasads { get; set; } = new List<TemplePrasad>();
        public virtual ICollection<TempleAarti> Aartis { get; set; } = new List<TempleAarti>();
        public virtual ICollection<TempleSchedule> TempleSchedules { get; set; } = new List<TempleSchedule>();
        public virtual ICollection<TempleException> TempleExceptions { get; set; } = new List<TempleException>();
        public ICollection<TempleLocalizedText> Localizations { get; set; } = new List<TempleLocalizedText>();

    }
}