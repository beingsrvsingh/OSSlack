using AstrologerMicroservice.Domain.Core.Repository;
using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace AstrologerMicroservice.Infrastructure.Repositories
{
    public class ExpertiesRepository : Repository<AstrologerExpertise>, IExpertiesRepository
    {
        private readonly AstrologerDbContext dbContext;

        public ExpertiesRepository(AstrologerDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
