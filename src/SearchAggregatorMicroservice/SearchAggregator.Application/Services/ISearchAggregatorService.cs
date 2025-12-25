using SearchAggregator.Application.Contracts;
using Shared.Application.Common.Contracts.Response;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Services
{
    public interface ISearchAggregatorService
    {
        Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
