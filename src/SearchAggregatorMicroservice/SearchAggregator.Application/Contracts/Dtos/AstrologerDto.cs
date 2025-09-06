using SearchAggregator.Application.Contracts.Interfaces;

namespace SearchAggregator.Application.Contracts.Dtos
{
    public class AstrologerDto : ISearchResult
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbnailUrl { get; set; } = default!;
        public double Price { get; set; }
        public string MatchType { get; set; } = "";
        public float Score { get; set; }
    }
}
