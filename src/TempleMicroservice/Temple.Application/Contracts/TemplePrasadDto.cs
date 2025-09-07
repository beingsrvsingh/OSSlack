namespace Temple.Application.Contracts
{
    public class TemplePrasadDto
    {
        public int TempleMasterId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

}
