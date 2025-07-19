using Identity.Domain.Core.Repository;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class UserInfoRepository : Repository<ApplicationUser>, IUserInfoRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserInfoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
