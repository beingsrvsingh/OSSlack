using Shared.Application.Common.Contracts.Response;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Services
{
    public interface IAggregatorService
    {
        Task<PagedResult<CatalogResponseDto>?> GetTrendingProductAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PagedResult<CatalogResponseDto>?> GetTrendingPriestAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PagedResult<CatalogResponseDto>?> GetTrendingAstrologerAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PagedResult<CatalogResponseDto>?> GetTrendingTempleAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PagedResult<CatalogResponseDto>?> GetTrendingKathavachakAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PagedResult<CatalogResponseDto>?> GetTrendingPoojaAsync(int page, int pageSize, CancellationToken cancellationToken);
    }
}
