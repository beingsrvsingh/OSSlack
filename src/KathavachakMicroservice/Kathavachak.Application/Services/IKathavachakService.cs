using Kathavachak.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakService
    {
        Task<CatalogResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<KathavachakMaster>> GetAllAsync();

        Task<List<CatalogResponseDto>?> GetKathavachaksBySubCategoryIdAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10);
        Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10);
        Task<List<CatalogResponseDto>> GetFilteredKathavachaksAsync(List<int> attributeIds, int? subCategoryId = null, int pageNumber = 1, int pageSize = 10);

        Task<bool> CreateAsync(KathavachakMaster entity);
        Task<bool> UpdateAsync(KathavachakMaster entity);
        Task<bool> DeleteAsync(int id);
        Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);

        Task<PagedResult<CatalogResponseDto>> GetTrendingProdcutsAsync(int pageNumber = 1, int pageSize = 10);
    }

}
