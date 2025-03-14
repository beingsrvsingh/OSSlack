using Logging.Domain.Repositories;

namespace Logging.Domain.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        ILogRepository LogRepository { get; }
        IAppsLogRepository AppsLogRepository { get; }
        void Save();
    }
}
