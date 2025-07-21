using SecretManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SecretManagement.Infrastructure.Persistence.Context;

public class SecretManagementDbContext : DbContext
{
    public SecretManagementDbContext(DbContextOptions<SecretManagementDbContext> options)
        : base(options) { }

    public virtual DbSet<Secret> Secrets => Set<Secret>();

    public virtual DbSet<ApiSecret> ApiSecrets => Set<ApiSecret>();

    public virtual DbSet<AuditLog> AuditLogs => Set<AuditLog>();

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
            Console.WriteLine($"An error occurred while applying configurations: {ex.InnerException}");
            throw;
        }
    }
}