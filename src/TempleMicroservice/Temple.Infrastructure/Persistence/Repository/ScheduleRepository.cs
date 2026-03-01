using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;
using Temple.Domain.Entities;
using Temple.Domain.Repositories;
using Temple.Infrastructure.Persistence.Context;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly TempleDbContext _context;

        public ScheduleRepository(TempleDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<List<Schedule>> GetSchedulesByDayAsync(int kathavachakId, DayOfWeek day)
        {
            return await _context.TempleSchedules
                .Where(x => x.TempleMasterId == kathavachakId
                         && (int)x.Day == (int)day
                         && x.IsAvailable)
                .ToListAsync();
        }

        public async Task<bool> IsFullDayBlockedAsync(int entityId, DateTime date)
        {
            return await _context.ScheduleExceptions
                .AnyAsync(x => x.TempleId == entityId
                            && x.Date == date
                            && x.IsBlocked
                            && x.StartTime == null
                            && x.EndTime == null);
        }

        public async Task<List<ScheduleException>> GetTimeExceptionsAsync(int entityId, DateTime date)
        {
            return await _context.ScheduleExceptions
                .Where(x => x.TempleId == entityId
                         && x.Date == date
                         && x.IsBlocked
                         && x.StartTime != null)
                .ToListAsync();
        }
    }
}