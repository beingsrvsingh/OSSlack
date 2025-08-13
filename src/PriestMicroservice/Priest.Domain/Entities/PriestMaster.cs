using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Priest.Domain.Entities.Enums;

namespace Priest.Domain.Entities
{
    public partial class PriestMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(36)]
        public string UserId { get; set; } = null!;

        [MaxLength(200)]
        public string? DisplayName { get; set; }

        [MaxLength(500)]
        public string? ProfilePictureUrl { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; } = 0m;

        public int TotalRatings { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ConsultationMode> ConsultationModes { get; set; } = new List<ConsultationMode>();

        public virtual ICollection<PriestExpertise> PriestExpertise { get; set; } = new List<PriestExpertise>();
        public virtual ICollection<PriestLanguage> PriestLanguages { get; set; } = new List<PriestLanguage>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
        public virtual ICollection<ServicePackage> RitualServicePackages { get; set; } = new List<ServicePackage>();
    }

}