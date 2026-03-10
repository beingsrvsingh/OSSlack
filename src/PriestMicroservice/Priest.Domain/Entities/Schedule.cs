using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        public int PriestId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster Priest { get; set; } = null!;
    }

}