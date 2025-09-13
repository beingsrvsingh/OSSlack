namespace Shared.Application.Contracts
{
    public class ProductSearchRawResultDto
    {
        public List<SearchItemDto> Results { get; set; } = new();        
        public BaseSearchFilterMetadata Filters { get; set; } = new();       
    }

    public class BaseSearchFilterMetadata
    {
        public int TotalCount { get; set; }
        public bool HasMoreResults { get; set; }
        public float Score { get; set; } 
        public string MatchType { get; set; } = "Partial";
        public string Source { get; set; } = "";
        public bool EnableFilters { get; set; }
    }
}
