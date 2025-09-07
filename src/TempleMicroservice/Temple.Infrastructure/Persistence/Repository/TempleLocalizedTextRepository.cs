using Shared.Infrastructure.Repositories;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;
using Temple.Infrastructure.Persistence.Context;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class TempleLocalizedTextRepository : Repository<TempleLocalizedText>, ITempleLocalizedTextRepository
    {
        private readonly TempleDbContext _context;
        public TempleLocalizedTextRepository(TempleDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
