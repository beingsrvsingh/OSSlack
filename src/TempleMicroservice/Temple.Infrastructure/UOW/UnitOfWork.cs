using Temple.Domain.Entities;
using Temple.Domain.Repositories;
using Temple.Domain.UOW;
using Temple.Infrastructure.Persistence.Context;
using Shared.Domain.Entities;
using Shared.Infrastructur.UoW;

namespace Temple.Infrastructure.UOW
{
    public class UnitOfWork : BaseUnitOfWork<TempleDbContext, AuditLog>, IUnitOfWork
    {
        public UnitOfWork(TempleDbContext dbContext,
        ITempleRepository astrologerRepo,
        IScheduleRepository scheduleRepo) : base(dbContext)
        {
            Temples = astrologerRepo;
            Schedules = scheduleRepo;
        }

        public ITempleRepository Temples { get; }
        public IScheduleRepository Schedules { get; }


        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}