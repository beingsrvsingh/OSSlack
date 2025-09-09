using Microsoft.EntityFrameworkCore;
using Priest.Domain.Core.Repository;
using PriestMicroservice.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class PriestExpertiseRepository : Repository<PriestExpertise>, IPriestExpertiseRepository
    {
        private readonly DbContext dbContext;

        public PriestExpertiseRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }

}
