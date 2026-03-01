using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Kathavachak.Infrastructure.Repositories
{
    public class KathavachakScheduleRepository : Repository<Schedule>, IKathavachakScheduleRepository
    {
        private readonly KathavachakDbContext _context;

        public KathavachakScheduleRepository(KathavachakDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Schedule>> GetSchedulesByDayAsync(int entityId, DayOfWeek day)
        {
            return await _context.KathavachakSchedules
                .Where(x => x.KathavachakId == entityId
                         && (int)x.Day == (int)day
                         && x.IsAvailable)
                .ToListAsync();
        }

        public async Task<bool> IsFullDayBlockedAsync(int entityId, DateTime date)
        {
            return await _context.ScheduleExceptions
                .AnyAsync(x => x.KathavachakId == entityId
                            && x.Date == date
                            && x.IsBlocked
                            && x.StartTime == null
                            && x.EndTime == null);
        }

        public async Task<List<ScheduleException>> GetTimeExceptionsAsync(int entityId, DateTime date)
        {
            return await _context.ScheduleExceptions
                .Where(x => x.KathavachakId == entityId
                         && x.Date == date
                         && x.IsBlocked
                         && x.StartTime != null)
                .ToListAsync();
        }
    }

}
