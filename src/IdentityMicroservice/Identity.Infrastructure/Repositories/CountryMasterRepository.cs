using Identity.Domain.Core.Repository;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class CountryMasterRepository : Repository<CountryMaster>, ICountryMasterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CountryMasterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
