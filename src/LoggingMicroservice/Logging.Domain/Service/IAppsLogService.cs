using Logging.Domain.Entities;

namespace Logging.Domain.Service
{
    public interface IAppsLogService
    {
        Task Add(AppsLog appsLog);
    }
}
