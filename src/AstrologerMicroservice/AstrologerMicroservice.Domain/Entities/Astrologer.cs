using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AstrologerMicroservice.Domain.Entities.Enums;

namespace AstrologerMicroservice.Domain.Entities
{    
    public partial class Astrologer
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

        public ConsultationMode ConsultationModes { get; set; } = ConsultationMode.None;
        public virtual ICollection<AstrologerLanguage> AstrologerLanguages { get; set; } = new List<AstrologerLanguage>();
        public virtual ICollection<AstrologerExpertise> AstrologerExpertises { get; set; } = new List<AstrologerExpertise>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
        public virtual ICollection<ServicePackage> ServicePackages { get; set; } = new List<ServicePackage>();
    }

}