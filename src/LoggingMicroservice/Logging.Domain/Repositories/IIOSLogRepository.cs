using Logging.Domain.Contracts;
using Logging.Domain.Entities;
using Shared.Domain.Contracts;
using Shared.Domain.Repository;

namespace Logging.Domain.Repositories
{
    public interface IIOSLogRepository : IRepository<IOSLog>
    {
        Task<PaginatedResult<IOSLogDto>> GetFilteredAsync(int page, int pageSize, string? logLevel = null, DateTime? from = null, DateTime? to = null);

    }
}
