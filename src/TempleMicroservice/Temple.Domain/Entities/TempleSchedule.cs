
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TempleSchedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TempleMasterId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan OpenTime { get; set; }

        [Required]
        public TimeSpan CloseTime { get; set; }

        public bool IsActive { get; set; } = true;

        [MaxLength(100)]
        public string? Reason { get; set; }

        [ForeignKey(nameof(TempleMasterId))]
        public virtual TempleMaster TempleMaster { get; set; } = null!;

        public virtual ICollection<TempleTimeSlot> TimeSlots { get; set; } = new List<TempleTimeSlot>();
    }
}