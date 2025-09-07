namespace Temple.Application.Contracts
{
    public class TempleMasterDto
    {
        public int LocationId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
