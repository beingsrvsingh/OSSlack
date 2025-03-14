using Identity.Domain.Core.Repository;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class AddressRepository : Repository<AspNetUserAddress>, IAddressRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AddressRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
