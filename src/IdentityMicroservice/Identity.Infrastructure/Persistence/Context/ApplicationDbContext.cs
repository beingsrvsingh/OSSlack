using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public virtual DbSet<AspNetUserInfo> AspNetUserInfos => Set<AspNetUserInfo>();
    public virtual DbSet<AspNetUserRefreshToken> AspNetUserRefreshTokens => Set<AspNetUserRefreshToken>();
    public virtual DbSet<AspNetUserAuditLog> AspNetUserAuditLogs => Set<AspNetUserAuditLog>();
    public virtual DbSet<AspNetUserDevice> AspNetUserDevices => Set<AspNetUserDevice>();
    public virtual DbSet<AspNetUserAddress> AspNetUserAddresses => Set<AspNetUserAddress>();
    public virtual DbSet<CityMaster> CityMasters => Set<CityMaster>();

    public virtual DbSet<CountryMaster> CountryMasters => Set<CountryMaster>();

    public virtual DbSet<StateMaster> StateMasters => Set<StateMaster>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
