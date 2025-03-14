using Identity.Domain.Core.Repository;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class StateMasterRepository : Repository<StateMaster>, IStateMasterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StateMasterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
