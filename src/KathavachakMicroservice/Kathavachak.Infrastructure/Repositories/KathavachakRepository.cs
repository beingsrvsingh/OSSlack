using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Kathavachak.Infrastructure.Repositories
{
    public class KathavachakRepository : Repository<KathavachakMaster>, IKathavachakRepository
    {
        private readonly KathavachakDbContext _context;

        public KathavachakRepository(KathavachakDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
