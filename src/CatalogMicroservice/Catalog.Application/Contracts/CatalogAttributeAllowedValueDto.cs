
namespace Catalog.Application.Contracts
{
    public class CatalogAttributeAllowedValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public int SortOrder { get; set; }
    }

}