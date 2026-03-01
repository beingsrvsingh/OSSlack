using Temple.Domain.Entities;
using Shared.Domain.Repository;

namespace Temple.Domain.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<List<Schedule>> GetSchedulesByDayAsync(int entityId, DayOfWeek day);
        Task<bool> IsFullDayBlockedAsync(int entityId, DateTime date);
        Task<List<ScheduleException>> GetTimeExceptionsAsync(int entityId, DateTime date);
    }
}