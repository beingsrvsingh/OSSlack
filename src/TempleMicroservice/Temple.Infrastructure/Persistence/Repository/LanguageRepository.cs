using Temple.Domain.Core.Repository;
using Temple.Domain.Entities;
using Temple.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Temple.Infrastructure.Repositories
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private readonly TempleDbContext dbContext;

        public LanguageRepository(TempleDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
