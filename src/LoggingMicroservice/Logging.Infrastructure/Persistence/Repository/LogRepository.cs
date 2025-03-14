using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using Logging.Infrastructure.Context;

namespace Logging.Infrastructure.Persistence.Repository
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        private readonly LoggerContext loggerContext;

        public LogRepository(LoggerContext loggerContext) : base(loggerContext)
        {
            this.loggerContext = loggerContext;
        }
    }
}
