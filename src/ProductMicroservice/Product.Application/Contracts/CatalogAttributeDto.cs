
namespace Product.Application.Contracts
{
    public class CatalogAttributeDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string DataType { get; set; } = null!;
        public bool IsCustom { get; set; }
        public bool IsRequired { get; set; }
        public int SortOrder { get; set; }

        public CatalogAttributeIconDto? Icon { get; set; }

        public List<CatalogAttributeAllowedValueDto>? AllowedValues { get; set; }
    }

    public class CatalogAttributeIconDto
    {
        public int Id { get; set; }
        public string IconName { get; set; } = null!;
        public int IconCodePoint { get; set; }
        public string IconFontFamily { get; set; } = null!;
    }

    public class CatalogAttributeAllowedValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public int SortOrder { get; set; }
    }

}