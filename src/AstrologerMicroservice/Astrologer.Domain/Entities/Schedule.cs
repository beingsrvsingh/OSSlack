
using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{
    public partial class Schedule
    {
        public int Id { get; set; }

        public int AstrologerId { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        [ForeignKey(nameof(AstrologerId))]
        public virtual AstrologerMaster Astrologer { get; set; } = null!;
    }

}