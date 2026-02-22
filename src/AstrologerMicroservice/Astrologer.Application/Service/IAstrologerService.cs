using Astrologer.Application.Contracts;
using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Entities.Enums;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Service
{
    public interface IAstrologerService
    {
        Task<IEnumerable<AstrologerMaster>> GetAvailableAsync(DateTime date, string language, string expertise);
        Task<CatalogResponseDto?> GetByIdAsync(int astrologerId);
        Task<AstrologerMaster?> GetByUserIdAsync(string userId);
        Task<IEnumerable<AstrologerMaster>> GetAllAsync(int page = 1, int pageNumber = 1, int pageSize = 10);

        Task<List<CatalogResponseDto>?> GetAstrologersBySubCategoryIdAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10);
        Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10);
        Task<List<CatalogResponseDto>> GetFilteredAstrologersAsync(List<int> attributeIds, int? subCategoryId = null, int pageNumber = 1, int pageSize = 10);

        Task<bool> CreateAsync(CreateAstrologerCommand command);
        Task<bool> UpdateAsync(UpdateAstrologerCommand command);
        Task<bool> DeleteAsync(int astrologerId);
        Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);

        Task<PagedResult<CatalogResponseDto>> GetTrendingProdcutsAsync(int pageNumber = 1, int pageSize = 10);
        Task<List<TimeSlotDto>> GetTodayAvailableSlotsAsync(int astrologerId, DateTime date);
    }
}