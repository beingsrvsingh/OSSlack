using Shared.Application.Contracts;

namespace SearchAggregator.Application.Clients
{
    public interface ITempleClient
    {
        Task<List<SearchResponseDto>> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
