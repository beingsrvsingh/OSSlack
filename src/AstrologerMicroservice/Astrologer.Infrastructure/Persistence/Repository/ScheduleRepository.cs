using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Repositories;
using AstrologerMicroservice.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace AstrologerMicroservice.Infrastructure.Persistence.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly AstrologerDbContext _context;

        public ScheduleRepository(AstrologerDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<List<Schedule>>GetSchedulesByDayAsync(int entityId, DayOfWeek day)
        {
            return await _context.Schedules
                .Where(x => x.AstrologerId == entityId
                         && (int)x.Day == (int)day
                         && x.IsAvailable)
                .ToListAsync();
        }

        public async Task<bool>IsFullDayBlockedAsync(int entityId, DateTime date)
        {
            return await _context.ScheduleExceptions
                .AnyAsync(x => x.AstrologerId == entityId
                            && x.Date == date
                            && x.IsBlocked
                            && x.StartTime == null
                            && x.EndTime == null);
        }

        public async Task<List<ScheduleException>>GetTimeExceptionsAsync(int entityId, DateTime date)
        {
            return await _context.ScheduleExceptions
                .Where(x => x.AstrologerId == entityId
                         && x.Date == date
                         && x.IsBlocked
                         && x.StartTime != null)
                .ToListAsync();
        }
    }
}