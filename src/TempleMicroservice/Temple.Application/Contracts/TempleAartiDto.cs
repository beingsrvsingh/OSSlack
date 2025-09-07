namespace Temple.Application.Contracts
{
    public class TempleAartiDto
    {
        public int TempleMasterId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public TimeSpan AartiTime { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
