using Logging.Domain.Contracts;
using Logging.Domain.Entities;
using Shared.Domain.Contracts;
using Shared.Domain.Repository;

namespace Logging.Domain.Repositories
{
    public interface IAndroidLogRepository : IRepository<AndroidLog>
    {
        Task<List<AndroidLog>> GetLatestLogsAsync(int count = 100);
        Task<PaginatedResult<AndroidLogDto>> GetFilteredAsync(int page, int pageSize, string? logLevel = null, DateTime? from = null, DateTime? to = null);
    }
}
