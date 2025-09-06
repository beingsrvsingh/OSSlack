using SearchAggregator.Application.Contracts;

namespace SearchAggregator.Application.Clients
{
    public interface IKathavachakClient
    {
        Task<KathavachakSearchResult> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
