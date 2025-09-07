using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakMaster
    {
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

        // Navigation
        public ICollection<KathavachakCategory> Categories { get; set; } = new List<KathavachakCategory>();
        public ICollection<KathavachakLanguage> Languages { get; set; } = new List<KathavachakLanguage>();
        public ICollection<KathavachakTopic> Topics { get; set; } = new List<KathavachakTopic>();
        public ICollection<KathavachakSessionMode> SessionModes { get; set; } = new List<KathavachakSessionMode>();
        public ICollection<KathavachakSchedule> Schedules { get; set; } = new List<KathavachakSchedule>();
        public ICollection<KathavachakTimeSlot> TimeSlots { get; set; } = new List<KathavachakTimeSlot>();
        public ICollection<KathavachakMedia> Media { get; set; } = new List<KathavachakMedia>();
    }

}
