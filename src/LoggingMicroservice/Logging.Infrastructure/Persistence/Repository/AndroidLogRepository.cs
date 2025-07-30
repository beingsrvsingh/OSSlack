using Logging.Domain.Contracts;
using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using Logging.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Contracts;
using Shared.Infrastructure.Repositories;

namespace Logging.Infrastructure.Persistence.Repository
{
    public class AndroidLogRepository : Repository<AndroidLog>, IAndroidLogRepository
    {
        private readonly LoggerDbContext dbContext;

        public AndroidLogRepository(LoggerDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<AndroidLog>> GetLatestLogsAsync(int count = 100)
        {
            return await this.dbContext.AndroidLogs
                .OrderByDescending(log => log.Timestamp)
                .Take(count)
                .ToListAsync();
        }

        public async Task<PaginatedResult<AndroidLogDto>> GetFilteredAsync(int page, int pageSize, string? logLevel = null, DateTime? from = null, DateTime? to = null)
        {
            var query = this.dbContext.AndroidLogs.AsQueryable();

            if (!string.IsNullOrEmpty(logLevel))
                query = query.Where(l => l.LogLevel == logLevel);

            if (from.HasValue)
                query = query.Where(l => l.Timestamp >= from.Value);

            if (to.HasValue)
                query = query.Where(l => l.Timestamp <= to.Value);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(l => l.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new AndroidLogDto
                {
                    Timestamp = l.Timestamp,
                    Message = l.Message,
                    LogLevel = l.LogLevel,
                    AndroidOsVersion = l.AndroidOsVersion,
                    AndroidDeviceModel = l.AndroidDeviceModel,
                    ExceptionMessage = l.ExceptionMessage
                })
                .ToListAsync();
            
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedResult<AndroidLogDto>(items, totalCount, page, pageSize, totalPages);
        }
    }
}
