using Logging.Domain.Contracts;
using Logging.Domain.Entities;
using Shared.Domain.Contracts;
using Shared.Domain.Repository;

namespace Logging.Domain.Repositories
{
    public interface IWebServiceLogRepository : IRepository<WebServiceLog>
    {
        Task<List<WebServiceLog>> GetLatestLogsAsync(int count = 100);
        Task<PaginatedResult<WebServiceLogDto>> GetFilteredAsync(int page, int pageSize, string? logLevel = null, DateTime? from = null, DateTime? to = null);
    }
}
