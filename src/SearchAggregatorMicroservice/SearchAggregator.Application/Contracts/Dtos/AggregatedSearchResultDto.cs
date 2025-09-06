namespace SearchAggregator.Application.Contracts.Dtos
{
    public class AggregatedSearchResultDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbnailUrl { get; set; } = default!;
        public double Price { get; set; }

        public string MatchType { get; set; } = ""; // e.g., Exact, Partial
        public string Source { get; set; } = "";    // e.g., Product, Astrologer
        public float Score { get; set; }            // relevance score for sorting

        public bool EnableFilter { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
    }

}
