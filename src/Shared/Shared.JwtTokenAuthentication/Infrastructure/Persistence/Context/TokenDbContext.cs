using JwtTokenAuthentication.Application;
using JwtTokenAuthentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Utilities.Cryptography;
using Utilities.Interfaces;

namespace JwtTokenAuthentication.Infrastructure.Persistence.Context;

public partial class TokenDbContext(DbContextOptions<TokenDbContext> options, IRegistryService registryService) : DbContext(options), ITokenDbContext
{
    private readonly IRegistryService registryService = registryService;

    public virtual DbSet<AspNetUserSecurityToken> AspNetUserSecurityTokens => Set<AspNetUserSecurityToken>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.
        UseSqlServer(Cryptography.DecryptString(registryService.GetConnectionString()), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
        .MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
