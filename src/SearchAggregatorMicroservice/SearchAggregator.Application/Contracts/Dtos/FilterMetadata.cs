namespace SearchAggregator.Application.Contracts.Dtos
{
    public class FilterMetadata
    {
        public bool EnableFilter { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool IsDirectMatch { get; set; }
    }
}
