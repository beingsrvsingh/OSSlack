using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public class TimeSlot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public bool IsBooked { get; set; } = false;

        [ForeignKey(nameof(ScheduleId))]
        public virtual Schedule Schedule { get; set; } = null!;
    }

}