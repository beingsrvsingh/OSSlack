using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Entities.Base
{
    public class BaseSearchRaw<T> where T: class
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public decimal? Price { get; set; }

        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public string? CategoryNameSnapshot { get; set; }
        public string? SubCategoryNameSnapshot { get; set; }

        public int? CatalogAttributeId { get; set; }
        public int? CatalogAttributeValueId { get; set; }
        public string?CatalogAttributeValue { get; set; }
        public string? CatalogAttributeKey { get; set; }
        public string? CatalogAttributeLabel { get; set; }
        public int? CatalogAttributeDatatype { get; set; }

        public List<BaseAttributeValue>? AttributeValues { get; set; } = new();

        public float? NameScore { get; set; }
        public float? CatScore { get; set; }
        public float? SubcatScore { get; set; }

        public float? TotalScore { get; set; }

        [NotMapped]
        public float Score => (NameScore ?? 0) + (CatScore ?? 0) + (SubcatScore ?? 0);

        [NotMapped]
        public string? MatchType { get; set; }
    }
}
