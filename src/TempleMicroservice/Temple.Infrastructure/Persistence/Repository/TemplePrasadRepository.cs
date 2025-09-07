using Shared.Infrastructure.Repositories;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;
using Temple.Infrastructure.Persistence.Context;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class TemplePrasadRepository : Repository<TemplePrasad>, ITemplePrasadRepository
    {
        private readonly TempleDbContext _context;
        public TemplePrasadRepository(TempleDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
