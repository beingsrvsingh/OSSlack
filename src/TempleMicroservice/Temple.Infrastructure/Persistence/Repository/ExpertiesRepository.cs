using Temple.Domain.Core.Repository;
using Temple.Domain.Entities;
using Temple.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Temple.Infrastructure.Repositories
{
    public class ExpertiesRepository : Repository<Expertise>, IExpertiesRepository
    {
        private readonly TempleDbContext dbContext;

        public ExpertiesRepository(TempleDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
