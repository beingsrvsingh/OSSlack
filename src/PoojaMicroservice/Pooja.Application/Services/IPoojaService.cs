using Pooja.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Utilities.Response;

namespace Pooja.Application.Services
{
    public interface IPoojaService
    {
        Task<CatalogResponseDto?> GetPoojaByIdAsync(int id);
        Task<IEnumerable<PoojaMaster>> GetAllPoojasAsync();
        Task<IEnumerable<PoojaMaster>> GetPoojasByTempleAsync(int templeId);
        Task<IEnumerable<PoojaMaster>> GetPoojasByPriestAsync(int priestId);

        Task<List<CatalogResponseDto>?> GetPoojasBySubCategoryIdAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10);
        Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10);
        Task<List<CatalogResponseDto>> GetFilteredPoojasAsync(List<int> attributeIds, int? subCategoryId = null, int pageNumber = 1, int pageSize = 10);

        Task AddPoojaAsync(PoojaMaster pooja);
        Task UpdatePoojaAsync(PoojaMaster pooja);
        Task DeletePoojaAsync(int id);

        // Optional: business-specific methods
        Task<IEnumerable<PoojaMaster>> SearchPoojasAsync(string keyword);
        Task<bool> IsPoojaAvailableAtHomeAsync(int poojaId);

        Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }

}
