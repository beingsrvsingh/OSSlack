using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Kathavachak.Infrastructure.Repositories
{
    public class KathavachakSessionModeRepository : Repository<KathavachakSessionMode>, IKathavachakSessionModeRepository
    {
        private readonly KathavachakDbContext _context;

        public KathavachakSessionModeRepository(KathavachakDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }

}
