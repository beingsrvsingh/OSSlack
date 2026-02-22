using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        public int KathavachakId { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
