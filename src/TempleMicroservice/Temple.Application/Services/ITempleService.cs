using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Utilities.Response;
using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITempleService
    {
        Task<IEnumerable<TempleMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<List<CatalogResponseDto>?> GetTemplesBySubCategoryIdAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10);
        Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10);
        Task<CatalogResponseDto?> GetTempleByIdWithDetailsAsync(int id);
        Task<List<CatalogResponseDto>> GetFilteredTemplesAsync(List<int> attributeIds, int? subCategoryId = null, int pageNumber = 1, int pageSize = 10);
        Task<bool> CreateAsync(TempleMaster temple);
        Task<bool> UpdateAsync(TempleMaster temple);
        Task<bool> DeleteAsync(int id);
        Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);

        Task<PagedResult<CatalogResponseDto>> GetTrendingProdcutsAsync(int pageNumber = 1, int pageSize = 10);
    }
}
