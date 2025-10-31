namespace Shared.Application.Common.Contracts
{
    public class BaseProductAttributeDto
    {
        public string Key { get; set; } = null!;
        public string Label { get; set; } = null!;
        // e.g. "String", "Enum", etc.
        public string DataType { get; set; } = null!;
        public List<string> Values { get; set; } = new();

        // From CatalogAttribute (optional but useful for UI)
        public BaseCatalogAttributeIconDto? Icon { get; set; }
    }
}
