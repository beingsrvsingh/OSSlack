namespace Shared.Application.Contracts
{
    public class SearchResultDto
    {
        public List<SearchItemDto> Results { get; set; } = new();
        public int TotalCount { get; set; }
        public bool HasMoreResults { get; set; }
        public float Score { get; set; }             // Top or avg score
        public string MatchType { get; set; } = "Partial"; // "Exact" or "Partial"
        public string Source { get; set; } = "";     // e.g. "Product", "Priest", etc.
        public bool EnableFilters { get; set; }
    }

}
