namespace Temple.Application.Contracts
{
    public class TempleLocalizedTextDto
    {
        public int TempleMasterId { get; set; }
        public string LanguageCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

}
