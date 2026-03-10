using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class PriestTempleService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PriestId { get; set; }
        public int PriestExpertiseId { get; set; }
        public int? PriestExpertiseModeId { get; set; }

        public int TempleId { get; set; }

        [MaxLength(150)]
        public string? PriestNameSnapshot { get; set; }
        [MaxLength(150)]
        public string? ExpertiseNameSnapshot { get; set; }
        [MaxLength(50)]
        public string? ModeNameSnapshot { get; set; }

        public bool IsActive { get; set; } = true;

        // Optional temporary block
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
