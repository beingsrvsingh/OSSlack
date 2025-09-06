using SearchAggregator.Application.Contracts.Dtos;

namespace SearchAggregator.Application.Contracts
{
    public class SearchResponse
    {
        public bool IsDirectMatch { get; set; }
        public List<AggregatedSearchResultDto> Results { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public FilterMetadata Filters { get; set; } = new();

    }
}
