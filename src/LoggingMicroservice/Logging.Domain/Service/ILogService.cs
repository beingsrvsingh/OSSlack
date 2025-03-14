using Logging.Domain.Entities;

namespace Logging.Domain.Service
{
    public interface ILogService
    {
        void Add(Log log);
    }
}
