using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using Logging.Domain.Service;

namespace Logging.Applicaton.Service
{
    public class AppsLogService : IAppsLogService
    {
        private readonly IAppsLogRepository repository;

        public AppsLogService(IAppsLogRepository repository)
        {
            this.repository = repository;
        }
        public Task Add(AppsLog appsLog)
        {
            repository.Add(appsLog);
            repository.Save();
            return Task.CompletedTask;
        }
    }
}
