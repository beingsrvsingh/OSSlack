
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain.Entities
{
    [Keyless]
    public class CatalogAttributeRaw
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public int CategoryMasterId { get; set; }
        public int SubCategoryMasterId { get; set; }
        public string Label { get; set; } = null!;
        public int? AllowedValueId { get; set; }
        public string? AllowedValue { get; set; }
        public int? AllowedValueSortOrder { get; set; }
        public int AttributeSortOrder { get; set; }
    }

}