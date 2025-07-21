using Identity.Domain.Entities;
using JwtTokenAuthentication.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public virtual DbSet<AspNetUserRefreshToken> AspNetUserRefreshTokens => Set<AspNetUserRefreshToken>();
    public virtual DbSet<AuditLog> AspNetUserAuditLogs => Set<AuditLog>();
    public virtual DbSet<AspNetUserDevice> AspNetUserDevices => Set<AspNetUserDevice>();
    public virtual DbSet<AspNetUserAddress> AspNetUserAddresses => Set<AspNetUserAddress>();
    public virtual DbSet<AspNetUserMembership> AspNetUserMemberships => Set<AspNetUserMembership>();
    public virtual DbSet<AspNetUserSigningKey> AspNetUserSigningKeys => Set<AspNetUserSigningKey>();
    public virtual DbSet<CityMaster> CityMasters => Set<CityMaster>();
    public virtual DbSet<CountryMaster> CountryMasters => Set<CountryMaster>();
    public virtual DbSet<StateMaster> StateMasters => Set<StateMaster>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        try
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        catch (System.Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"An error occurred while applying configurations: {""} entities found. Exception: {ex.InnerException}");
            throw;
        }
    }
}
