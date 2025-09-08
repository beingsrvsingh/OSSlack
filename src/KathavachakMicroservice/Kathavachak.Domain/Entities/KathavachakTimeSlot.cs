using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakTimeSlot
    {
        [Key]
        public int Id { get; set; }

        public int KathavachakId { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }

        public bool IsBooked { get; set; }

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
