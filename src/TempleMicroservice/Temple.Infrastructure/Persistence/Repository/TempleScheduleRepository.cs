using Shared.Infrastructure.Repositories;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;
using Temple.Infrastructure.Persistence.Context;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class TempleScheduleRepository : Repository<TempleSchedule>, ITempleScheduleRepository
    {
        private readonly TempleDbContext _context;
        public TempleScheduleRepository(TempleDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
