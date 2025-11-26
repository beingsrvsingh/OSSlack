using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITempleService
    {
        Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<List<CatalogResponseDto>?> GetTemplesBySubCategoryIdAsync(int? subCategoryId, int topN = 5);
        Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int topN = 5);
        Task<CatalogResponseDto?> GetTempleByIdWithDetailsAsync(int id);
        Task<List<CatalogResponseDto>> GetFilteredTemplesAsync(List<int> attributeIds, int? subCategoryId = null, int topN = 10);
        Task<bool> CreateAsync(TempleMaster temple);
        Task<bool> UpdateAsync(TempleMaster temple);
        Task<bool> DeleteAsync(int id);
        Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
