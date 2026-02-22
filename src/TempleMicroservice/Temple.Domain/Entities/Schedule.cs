
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        public int TempleMasterId { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        [ForeignKey(nameof(TempleMasterId))]
        public virtual TempleMaster TempleMaster { get; set; } = null!;
    }
}