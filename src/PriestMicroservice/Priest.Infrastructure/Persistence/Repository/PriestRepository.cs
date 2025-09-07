using Microsoft.EntityFrameworkCore;
using Priest.Domain.Core.Repository;
using Priest.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class PriestRepository : Repository<PriestMaster>, IPriestRepository
    {
        private readonly DbContext dbContext;

        public PriestRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        // Add any Priest-specific methods here
    }

}
