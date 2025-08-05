using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Repositories;
using AstrologerMicroservice.Domain.UOW;
using AstrologerMicroservice.Infrastructure.Persistence.Context;
using Shared.Domain.Entities;
using Shared.Infrastructur.UoW;

namespace AstrologerMicroservice.Infrastructure.UOW
{
    public class UnitOfWork : BaseUnitOfWork<AstrologerDbContext, AuditLog>, IUnitOfWork
    {
        public UnitOfWork(AstrologerDbContext dbContext,
        IAstrologerRepository astrologerRepo,
        IScheduleRepository scheduleRepo) : base(dbContext)
        {
            Astrologers = astrologerRepo;
            Schedules = scheduleRepo;
        }

        public IAstrologerRepository Astrologers { get; }
        public IScheduleRepository Schedules { get; }


        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}