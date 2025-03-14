using Identity.Domain.Core.Repository;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class RefreshTokenRepository : Repository<AspNetUserRefreshToken>, IRefreshTokenRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
