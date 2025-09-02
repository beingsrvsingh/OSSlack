
namespace Catalog.Application.Contracts
{
    public class FilterableAttributeDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string Label { get; set; } = null!;
        public List<CatalogAttributeAllowedValueDto > AllowedValues { get; set; } = new();
    }

    public class FilterAttributeGroupDto
    {
        public int Cid { get; set; }
        public int Scid { get; set; }
        public List<FilterableAttributeDto> Attributes { get; set; } = new();
    }


}