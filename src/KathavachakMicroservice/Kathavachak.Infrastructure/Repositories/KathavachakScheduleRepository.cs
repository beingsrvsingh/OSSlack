using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Kathavachak.Infrastructure.Repositories
{
    public class KathavachakScheduleRepository : Repository<KathavachakSchedule>, IKathavachakScheduleRepository
    {
        private readonly KathavachakDbContext _context;

        public KathavachakScheduleRepository(KathavachakDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }

}
