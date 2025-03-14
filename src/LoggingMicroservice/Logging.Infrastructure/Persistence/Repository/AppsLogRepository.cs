using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using Logging.Infrastructure.Context;

namespace Logging.Infrastructure.Persistence.Repository
{
    public class AppsLogRepository : Repository<AppsLog>, IAppsLogRepository
    {
        private readonly LoggerContext loggerContext;

        public AppsLogRepository(LoggerContext loggerContext) : base(loggerContext)
        {
            this.loggerContext = loggerContext;
        }
    }
}
