using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Entities.Base
{
    public class BaseSearchRaw<T> where T: class
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ThumbnailUrl { get; set; }
        public decimal? Price { get; set; }

        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }

        public List<BaseAttributeValue>? AttributeValues { get; set; } = new();

        public float? NameScore { get; set; }

        [NotMapped]
        public float Score => (NameScore ?? 0);

        [NotMapped]
        public string? MatchType { get; set; }
        public int TotalCount { get; set; }
    }
}
