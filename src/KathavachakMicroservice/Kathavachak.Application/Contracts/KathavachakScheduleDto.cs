namespace Kathavachak.Application.Contracts
{
    public class KathavachakScheduleDto
    {
        public int Id { get; set; }
        public int KathavachakId { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}
