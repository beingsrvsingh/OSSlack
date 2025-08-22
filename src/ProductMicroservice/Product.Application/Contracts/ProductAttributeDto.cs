
namespace Product.Application.Contracts
{
    public class ProductAttributeDto
    {
        public string Key { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string DataType { get; set; } = null!; // e.g. "String", "Enum", etc.

        // From CatalogAttribute (optional but useful for UI)
        public CatalogAttributeIconDto? Icon { get; set; }

        // If the attribute is of Enum type, include allowed values
        public List<CatalogAttributeAllowedValueDto>? AllowedValues { get; set; }
    }


}