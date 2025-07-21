using Identity.Domain.Core.Repository;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Context;
using JwtTokenAuthentication.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class UserSigningKeyRepository : Repository<AspNetUserSigningKey>, IUserSigningKeyRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserSigningKeyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
