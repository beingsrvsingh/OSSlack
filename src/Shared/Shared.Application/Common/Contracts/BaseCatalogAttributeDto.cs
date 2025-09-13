namespace Shared.Application.Common.Contracts
{
    public class BaseCatalogAttributeDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string DataType { get; set; } = null!;
        public bool IsCustom { get; set; }
        public bool IsRequired { get; set; }
        public int SortOrder { get; set; }

        public BaseCatalogAttributeIconDto? Icon { get; set; }

        public List<BaseCatalogAttributeAllowedValueDto>? AllowedValues { get; set; }
    }
}
