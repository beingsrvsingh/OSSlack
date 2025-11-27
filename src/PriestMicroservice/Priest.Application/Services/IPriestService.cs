using PriestMicroservice.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;

namespace Priest.Application.Services
{
    public interface IPriestService
    {
        // PriestMaster operations
        Task<CatalogResponseDto?> GetPriestByIdAsync(int id);
        Task<IEnumerable<PriestMaster>> GetAllActivePriestsAsync();
        Task AddPriestAsync(PriestMaster priest);
        Task UpdatePriestAsync(PriestMaster priest);
        Task DeletePriestAsync(int id);

        Task<List<CatalogResponseDto>?> GetPriestsBySubCategoryIdAsync(int? subCategoryId, int topN = 5);
        Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int topN = 5);
        Task<List<CatalogResponseDto>> GetFilteredAsync(List<int> attributeIds, int? subCategoryId = null, int topN = 10);

        // ConsultationMode
        Task<IEnumerable<ConsultationMode>> GetConsultationModesByPriestIdAsync(int expertiseId);

        // Expertise
        Task<IEnumerable<PriestExpertise>> GetExpertiseByPriestIdAsync(int priestId);

        // Languages
        Task<IEnumerable<PriestLanguage>> GetLanguagesByPriestIdAsync(int priestId);

        // Schedules & TimeSlots
        Task<IEnumerable<Schedule>> GetSchedulesByPriestIdAsync(int priestId);
        Task<IEnumerable<TimeSlot>> GetTimeSlotsByScheduleIdAsync(int scheduleId);
        Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
