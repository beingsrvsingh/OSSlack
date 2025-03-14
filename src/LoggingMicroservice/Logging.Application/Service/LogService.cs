using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using Logging.Domain.Service;

namespace Logging.Applicaton.Service
{
    public class LogService : ILogService
    {
        private readonly ILogRepository repository;

        public LogService(ILogRepository repository)
        {
            this.repository = repository;
        }

        public void Add(Log log)
        {
            repository.Add(log);
            repository.Save();
        }
    }
}
