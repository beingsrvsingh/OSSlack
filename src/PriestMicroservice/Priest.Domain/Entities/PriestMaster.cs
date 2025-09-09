using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public partial class PriestMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(36)]
        public string UserId { get; set; } = null!;

        [Required, MaxLength(36)]
        public string TempleId { get; set; } = null!;

        [MaxLength(200)]
        public string? Name { get; set; }

        [MaxLength(500)]
        public string? ThumbnailUrl { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; } = 0m;

        public int TotalRatings { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        public virtual ICollection<PriestExpertise> PriestExpertise { get; set; } = new List<PriestExpertise>();
        public virtual ICollection<PriestLanguage> PriestLanguages { get; set; } = new List<PriestLanguage>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }

}