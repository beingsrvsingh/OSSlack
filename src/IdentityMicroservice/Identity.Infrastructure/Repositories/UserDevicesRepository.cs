using Identity.Domain.Core.Repository;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class UserDevicesRepository : Repository<AspNetUserDevice>, IUserDevicesRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserDevicesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
