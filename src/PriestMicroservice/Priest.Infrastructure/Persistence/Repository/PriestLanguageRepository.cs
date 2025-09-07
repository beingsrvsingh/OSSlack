using Microsoft.EntityFrameworkCore;
using Priest.Domain.Core.Repository;
using Priest.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class PriestLanguageRepository : Repository<PriestLanguage>, IPriestLanguageRepository
    {
        private readonly DbContext dbContext;

        public PriestLanguageRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }

}
