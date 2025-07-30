using Logging.Domain.Repositories;
using Shared.Domain.Common.Entities.Interface;
using Shared.Domain.UOW;

namespace Logging.Domain.UOW
{
    public interface IUnitOfWork :  IBaseUnitOfWork, IAuditLog, IDisposable
    {
        IAndroidLogRepository AndroidLogRepository { get; }
        IIOSLogRepository IOSLogRepository { get; }
        IWebServiceLogRepository WebServiceLogRepository { get; }
    }
}
