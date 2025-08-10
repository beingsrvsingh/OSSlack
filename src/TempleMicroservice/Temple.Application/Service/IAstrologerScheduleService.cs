using Temple.Domain.Entities;

namespace Temple.Application.Service
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