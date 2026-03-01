using Priest.Domain.Entities;
using PriestMicroservice.Domain.Entities;
using Shared.Domain.Repository;


namespace Priest.Domain.Core.Repository
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<List<Schedule>> GetSchedulesByDayAsync(int entityId, DayOfWeek day);
        Task<bool> IsFullDayBlockedAsync(int entityId, DateTime date);
        Task<List<ScheduleException>> GetTimeExceptionsAsync(int entityId, DateTime date);
    }
}
