using Temple.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public partial class ScheduleException
    {
        public int Id { get; set; }

        public int TempleId { get; set; }

        // Date of exception
        public DateTime Date { get; set; }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        // True if unavailable
        public bool IsBlocked { get; set; } = true;

        [ForeignKey(nameof(TempleId))]
        public virtual TempleMaster Temple { get; set; } = null!;
    }
}
