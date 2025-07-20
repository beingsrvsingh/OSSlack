using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities;
using System.Reflection;

namespace Review.Infrastructure.Persistence.Context;

public partial class ReviewDbContext(DbContextOptions<ReviewDbContext> options) : DbContext(options)
{
    public virtual DbSet<Reviews> Reviews => Set<Reviews>();
    public virtual DbSet<ReviewReportLookup> ReportTypes => Set<ReviewReportLookup>();
    public virtual DbSet<ReviewDetail> ReviewDetails => Set<ReviewDetail>();
    public virtual DbSet<AuditLog> ReviewAuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
