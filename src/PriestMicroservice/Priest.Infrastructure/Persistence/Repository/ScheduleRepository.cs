using Microsoft.EntityFrameworkCore;
using Priest.Domain.Core.Repository;
using Priest.Domain.Entities;
using Priest.Infrastructure.Persistence.Context;
using PriestMicroservice.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly PriestDbContext _context;

        public ScheduleRepository(PriestDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<List<Schedule>> GetSchedulesByDayAsync(int entityId, DayOfWeek day)
        {
            return await _context.Schedules
                .Where(x => x.PriestId == entityId
                         && (int)x.DayOfWeek == (int)day
                         && x.IsAvailable)
                .ToListAsync();
        }

        public async Task<bool> IsFullDayBlockedAsync(int entityId, DateTime date)
        {
            return await _context.ScheduleExceptions
                .AnyAsync(x => x.PriestId == entityId
                            && x.Date == date
                            && x.IsBlocked
                            && x.StartTime == null
                            && x.EndTime == null);
        }

        public async Task<List<ScheduleException>> GetTimeExceptionsAsync(int entityId, DateTime date)
        {
            return await _context.ScheduleExceptions
                .Where(x => x.PriestId == entityId
                         && x.Date == date
                         && x.IsBlocked
                         && x.StartTime != null)
                .ToListAsync();
        }
    }

}
