using JwtTokenAuthentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtTokenAuthentication.Application
{
    public interface ITokenDbContext
    {
        DbSet<AspNetUserSecurityToken> AspNetUserSecurityTokens { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
