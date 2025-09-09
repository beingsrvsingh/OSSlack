using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{
    public partial class TimeSlot
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }

        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }

        public bool IsBooked { get; set; }

        [ForeignKey(nameof(ScheduleId))]
        public virtual Schedule Schedule { get; set; } = null!;
    }

}