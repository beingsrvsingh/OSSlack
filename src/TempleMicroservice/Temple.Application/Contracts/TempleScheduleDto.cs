namespace Temple.Application.Contracts
{
    public class TempleScheduleDto
    {
        public int TempleMasterId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
