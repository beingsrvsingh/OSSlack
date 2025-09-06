namespace SearchAggregator.Application.Contracts.Dtos
{
    public class FilterMetadata
    {
        public bool EnableFilter { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
    }
}
