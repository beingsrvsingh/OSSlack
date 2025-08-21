
namespace Catalog.Application.Contracts
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
    }

}