using SearchAggregator.Application.Contracts;

namespace SearchAggregator.Application.Clients
{
    public interface ITempleClient
    {
        Task<TempleSearchResult> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
