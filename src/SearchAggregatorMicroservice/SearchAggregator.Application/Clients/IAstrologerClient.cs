using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Clients
{
    public interface IAstrologerClient
    {
        Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);

        Task<PagedResult<CatalogResponseDto>> GetTrendingAstrologerAsync(int page, int pageSize, CancellationToken cancellationToken);
    }
}
