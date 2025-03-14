using Logging.Domain.Repositories;
using Logging.Domain.UOW;
using Logging.Infrastructure.Context;
using Logging.Infrastructure.Persistence.Repository;

namespace Logging.Infrastructure.UOW
{
    public class UnitOfWork(LoggerContext context) : IUnitOfWork
    {
        private readonly LoggerContext _dbContext = context;
        private IAppsLogRepository? appsLogRepository;
        private ILogRepository? logRepository;

        public ILogRepository LogRepository
        {
            get
            {
                if (logRepository == null)
                {
                    logRepository = new LogRepository(_dbContext);
                }
                return logRepository;
            }
        }

        public IAppsLogRepository AppsLogRepository
        {
            get
            {
                if (appsLogRepository == null)
                {
                    appsLogRepository = new AppsLogRepository(_dbContext);
                }
                return appsLogRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
