using AstrologerMicroservice.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Astrologer.Domain.Entities
{
    public partial class ScheduleException
    {
        public int Id { get; set; }

        public int AstrologerId { get; set; }

        // Date of exception
        public DateTime Date { get; set; }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        // True if unavailable
        public bool IsBlocked { get; set; } = true;

        [ForeignKey(nameof(AstrologerId))]
        public virtual AstrologerMaster Astrologer { get; set; } = null!;
    }
}
