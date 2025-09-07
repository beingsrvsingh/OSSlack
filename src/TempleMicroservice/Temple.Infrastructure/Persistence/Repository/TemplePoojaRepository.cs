using Shared.Infrastructure.Repositories;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;
using Temple.Infrastructure.Persistence.Context;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class TemplePoojaRepository : Repository<TemplePooja>, ITemplePoojaRepository
    {
        private readonly TempleDbContext _context;
        public TemplePoojaRepository(TempleDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
