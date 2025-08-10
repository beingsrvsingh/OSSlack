
namespace Temple.Domain.Entities
{
    public partial class Schedule
    {
        public int Id { get; set; }

        public int AstrologerId { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        public virtual TempleMaster TempleMaster { get; set; } = null!;
    }

}