namespace Shared.Application.Contracts
{
    public class SearchItemDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbnailUrl { get; set; } = default!;
        public double Price { get; set; }
        public float Score { get; set; }
        public string MatchType { get; set; } = "Partial";
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
    }
}
