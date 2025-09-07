using Shared.Infrastructure.Repositories;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;
using Temple.Infrastructure.Persistence.Context;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class TempleDonationRepository : Repository<TempleDonation>, ITempleDonationRepository
    {
        private readonly TempleDbContext _context;
        public TempleDonationRepository(TempleDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
