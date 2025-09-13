namespace Shared.Application.Common.Contracts
{
    public class BaseCatalogAttributeIconDto
    {
        public int Id { get; set; }
        public string IconName { get; set; } = null!;
        public int IconCodePoint { get; set; }
        public string IconFontFamily { get; set; } = null!;
    }
}
