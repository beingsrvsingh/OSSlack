using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Kathavachak.Infrastructure.Repositories
{
    public class KathavachakTopicRepository : Repository<KathavachakTopic>, IKathavachakTopicRepository
    {
        private readonly KathavachakDbContext _context;

        public KathavachakTopicRepository(KathavachakDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }

}
