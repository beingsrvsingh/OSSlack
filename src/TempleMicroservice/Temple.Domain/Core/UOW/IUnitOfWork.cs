using Temple.Domain.Repositories;
using Shared.Domain.Common.Entities.Interface;
using Shared.Domain.UOW;

namespace Temple.Domain.UOW
{
    public interface IUnitOfWork : IBaseUnitOfWork, IAuditLog, IDisposable
    {
        ITempleRepository Temples { get; }
        IScheduleRepository Schedules { get; }
    }
}