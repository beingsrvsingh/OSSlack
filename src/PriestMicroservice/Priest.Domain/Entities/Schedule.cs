using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Priest.Domain.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PriestId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        public bool IsAvailable { get; set; } = true;

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster Priest { get; set; } = null!;

        public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
    }

}