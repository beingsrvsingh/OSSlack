using PriestMicroservice.Domain.Entities;
using Shared.Application.Contracts;

namespace Priest.Application.Services
{
    public interface IPriestService
    {
        // PriestMaster operations
        Task<PriestMaster?> GetPriestByIdAsync(int id);
        Task<IEnumerable<PriestMaster>> GetAllActivePriestsAsync();
        Task AddPriestAsync(PriestMaster priest);
        Task UpdatePriestAsync(PriestMaster priest);
        Task DeletePriestAsync(int id);

        // ConsultationMode
        Task<IEnumerable<ConsultationMode>> GetConsultationModesByPriestIdAsync(int expertiseId);

        // Expertise
        Task<IEnumerable<PriestExpertise>> GetExpertiseByPriestIdAsync(int priestId);

        // Languages
        Task<IEnumerable<PriestLanguage>> GetLanguagesByPriestIdAsync(int priestId);

        // Schedules & TimeSlots
        Task<IEnumerable<Schedule>> GetSchedulesByPriestIdAsync(int priestId);
        Task<IEnumerable<TimeSlot>> GetTimeSlotsByScheduleIdAsync(int scheduleId);
        Task<SearchResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
