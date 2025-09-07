using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Kathavachak.Infrastructure.Repositories
{
    public class KathavachakLanguageRepository : Repository<KathavachakLanguage>, IKathavachakLanguageRepository
    {
        private readonly KathavachakDbContext _context;

        public KathavachakLanguageRepository(KathavachakDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }

}
