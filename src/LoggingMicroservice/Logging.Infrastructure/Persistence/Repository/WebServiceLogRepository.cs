using Logging.Domain.Contracts;
using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using Logging.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Contracts;
using Shared.Infrastructure.Repositories;

namespace Logging.Infrastructure.Persistence.Repository
{
    public class WebServiceLogRepository : Repository<WebServiceLog>, IWebServiceLogRepository
    {
        private readonly LoggerDbContext dbContext;

        public WebServiceLogRepository(LoggerDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<WebServiceLog>> GetLatestLogsAsync(int count = 100)
        {
            return await this.dbContext.WebServiceLogs
                .OrderByDescending(log => log.Timestamp)
                .Take(count)
                .ToListAsync();
        }

        public async Task<PaginatedResult<WebServiceLogDto>> GetFilteredAsync(int page, int pageSize, string? logLevel = null, DateTime? from = null, DateTime? to = null)
        {
            var query = this.dbContext.WebServiceLogs.AsQueryable();

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
                .Select(l => new WebServiceLogDto
                {
                    Timestamp = l.Timestamp,
                    Message = l.Message,
                    LogLevel = l.LogLevel,
                    Endpoint = l.Endpoint,
                    HttpStatusCode = l.HttpStatusCode,
                    ExceptionDetails = l.ExceptionDetails
                })
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedResult<WebServiceLogDto>(items, totalCount, page, pageSize, totalPages);
        }
    }
}