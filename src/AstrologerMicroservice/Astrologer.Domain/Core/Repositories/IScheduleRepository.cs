using AstrologerMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace AstrologerMicroservice.Domain.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<List<Schedule>> GetSchedulesByDayAsync(int astrologerId, DayOfWeek day);

        Task<bool> IsFullDayBlockedAsync(int astrologerId, DateTime date);

        Task<List<ScheduleException>>GetTimeExceptionsAsync(int astrologerId, DateTime date);
    }
}