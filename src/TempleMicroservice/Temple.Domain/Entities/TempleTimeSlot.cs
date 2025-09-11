using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TempleTimeSlot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        [Required]
        public DateTime SlotStartTime { get; set; }

        [Required]
        public DateTime SlotEndTime { get; set; }

        public int MaxCapacity { get; set; } = 0;

        public int BookedCount { get; set; } = 0;

        [MaxLength(100)]
        public string? Label { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        [ForeignKey(nameof(ScheduleId))]
        public virtual TempleSchedule TempleSchedule { get; set; } = null!;
    }

}
