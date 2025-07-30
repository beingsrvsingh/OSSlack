using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using Logging.Domain.UOW;
using Logging.Infrastructure.Context;
using Logging.Infrastructure.Persistence.Repository;
using Mapster;
using Shared.Domain.Entities;
using Shared.Infrastructur.UoW;

namespace Logging.Infrastructure.UOW
{
    public class UnitOfWork : BaseUnitOfWork<LoggerDbContext, AuditLog>, IUnitOfWork
    {
        public UnitOfWork(LoggerDbContext dbContext) : base(dbContext)
        { }

        private IAndroidLogRepository? androidLogRepository;
        private IIOSLogRepository? iOSLogRepository;
        private IWebServiceLogRepository? webServiceLogRepository;

        public IAndroidLogRepository AndroidLogRepository
        {
            get
            {
                if (androidLogRepository == null)
                {
                    androidLogRepository = new AndroidLogRepository(_context);
                }
                return androidLogRepository;
            }
        }

        public IIOSLogRepository IOSLogRepository
        {
            get
            {
                if (iOSLogRepository == null)
                {
                    iOSLogRepository = new IOSLogRepository(_context);
                }
                return iOSLogRepository;
            }
        }

        public IWebServiceLogRepository WebServiceLogRepository
        {
            get
            {
                if (webServiceLogRepository == null)
                {
                    webServiceLogRepository = new WebServiceLogRepository(_context);
                }
                return webServiceLogRepository;
            }
        }

        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            return entry.ToAudit().Adapt<AuditLog>();
        }
    }
}
