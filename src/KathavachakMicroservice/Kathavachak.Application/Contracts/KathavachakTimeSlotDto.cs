namespace Kathavachak.Application.Contracts
{
    public class KathavachakTimeSlotDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
    }
}
