using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Kathavachak.Infrastructure.Repositories
{
    public class KathavachakCategoryRepository : Repository<KathavachakExpertise>, IKathavachakCategoryRepository
    {
        private readonly KathavachakDbContext _context;

        public KathavachakCategoryRepository(KathavachakDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }

}
