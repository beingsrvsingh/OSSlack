using Shared.Application.Contracts;

namespace SearchAggregator.Application.Clients
{
    public interface IPriestClient
    {
        Task<SearchResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
