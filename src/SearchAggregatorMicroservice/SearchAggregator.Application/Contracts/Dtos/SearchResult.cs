using SearchAggregator.Application.Contracts.Interfaces;

namespace SearchAggregator.Application.Contracts
{
    public class SearchResult<T> where T : ISearchResult
    {
        public bool HasMoreResults { get; set; }
        public int TotalCount { get; set; }
        public string Source { get; set; }
        public string MatchType { get; set; } = "Partial";
        public float Score { get; set; }
        public List<T> Results { get; set; } = new();
    }

}
