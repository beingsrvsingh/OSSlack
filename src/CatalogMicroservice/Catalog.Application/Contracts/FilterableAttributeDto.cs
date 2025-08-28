
namespace Catalog.Application.Contracts
{
    public class FilterableAttributeDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public int? CategoryMasterId { get; set; }
        public int? SubCategoryMasterId { get; set; }
        public string Label { get; set; } = null!;
        public List<string> AllowedValues { get; set; } = new();
    }

}