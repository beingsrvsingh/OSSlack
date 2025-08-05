using AstrologerMicroservice.Domain.Repositories;
using Shared.Domain.Common.Entities.Interface;
using Shared.Domain.UOW;

namespace AstrologerMicroservice.Domain.UOW
{
    public interface IUnitOfWork : IBaseUnitOfWork, IAuditLog, IDisposable
    {
        IAstrologerRepository Astrologers { get; }
        IScheduleRepository Schedules { get; }
    }
}