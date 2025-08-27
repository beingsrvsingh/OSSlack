
namespace Catalog.Application.Contracts
{
    public class CatalogAttributeDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string DataType { get; set; } = null!;
        public bool IsFilterable { get; set; } = false;
        public bool IsSummary { get; set; } = false;    
        public bool IsCustom { get; set; }
        public bool IsRequired { get; set; }
        public int SortOrder { get; set; }
        public CatalogAttributeIconDto? Icon { get; set; }
        public List<CatalogAttributeAllowedValueDto>? AllowedValues { get; set; }

    }

}