using AstrologerMicroservice.Domain.Core.Repository;
using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace AstrologerMicroservice.Infrastructure.Repositories
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private readonly AstrologerDbContext dbContext;

        public LanguageRepository(AstrologerDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
