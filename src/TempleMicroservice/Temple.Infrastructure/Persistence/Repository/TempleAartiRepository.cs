using Shared.Infrastructure.Repositories;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;
using Temple.Infrastructure.Persistence.Context;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class TempleAartiRepository : Repository<TempleAarti>, ITempleAartiRepository
    {
        private readonly TempleDbContext _context;
        public TempleAartiRepository(TempleDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
