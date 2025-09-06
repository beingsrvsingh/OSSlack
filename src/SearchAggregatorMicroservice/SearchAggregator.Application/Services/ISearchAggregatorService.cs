using SearchAggregator.Application.Contracts;

namespace SearchAggregator.Application.Services
{
    public interface ISearchAggregatorService
    {
        Task<SearchResponse> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
