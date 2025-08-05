using AstrologerMicroservice.Domain.Entities;

namespace AstrologerMicroservice.Application.Service
{
    public interface IAstrologerScheduleService
    {
        Task<IEnumerable<Schedule>> GetByAstrologerIdAsync(int astrologerId);
        Task<Schedule?> GetByIdAsync(int scheduleId);

        Task<Schedule> CreateAsync(Schedule schedule);
        Task<bool> UpdateAsync(Schedule schedule);
        Task<bool> DeleteAsync(int scheduleId);

        Task<bool> SetSchedulesAsync(int astrologerId, IEnumerable<Schedule> schedules);
    }
}