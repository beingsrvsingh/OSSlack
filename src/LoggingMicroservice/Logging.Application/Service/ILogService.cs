using Logging.Domain.Contracts;
using Logging.Domain.Entities;
using Shared.Domain.Contracts;

namespace Logging.Application.Service
{
    public interface ILogService
    {
        // Write operations
        Task<bool> LogAndroidAsync(AndroidLog log);
        Task<bool> LogIOSAsync(IOSLog log);
        Task<bool> LogWebServiceAsync(WebServiceLog log);

        // Read operations
        Task<PaginatedResult<AndroidLogDto>> GetAndroidLogsAsync(int page, int pageSize);
        Task<PaginatedResult<IOSLogDto>> GetIOSLogsAsync(int page, int pageSize);
        Task<PaginatedResult<WebServiceLogDto>> GetWebServiceLogsAsync(int page, int pageSize);
    }

}
