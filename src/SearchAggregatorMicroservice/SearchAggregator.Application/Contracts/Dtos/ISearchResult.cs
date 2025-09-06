namespace SearchAggregator.Application.Contracts.Interfaces
{
    public interface ISearchResult
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        string ThumbnailUrl { get; }
        double Price { get; set; }
        string MatchType { get; set; }
        float Score { get; set; }
    }

}
