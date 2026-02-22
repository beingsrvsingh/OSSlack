using PriestMicroservice.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Priest.Domain.Entities
{
    public partial class ScheduleException
    {
        public int Id { get; set; }

        public int PriestId { get; set; }

        // Date of exception
        public DateTime Date { get; set; }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        // True if unavailable
        public bool IsBlocked { get; set; } = true;

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster Priest { get; set; } = null!;
    }
}
