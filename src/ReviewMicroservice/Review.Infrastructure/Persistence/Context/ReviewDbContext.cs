using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities;
using System.Reflection;

using ReviewEntity = Review.Domain.Entities.Review;

namespace Review.Infrastructure.Persistence.Context;

public partial class ReviewDbContext(DbContextOptions<ReviewDbContext> options) : DbContext(options)
{
    public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
    public DbSet<ReviewMedia> ReviewMedia => Set<ReviewMedia>();
    public DbSet<ReviewReport> ReviewReports => Set<ReviewReport>();
    public DbSet<ReviewFeedback> ReviewFeedbacks => Set<ReviewFeedback>();
    public virtual DbSet<ReviewReportReason> ReportReasons => Set<ReviewReportReason>();
    public virtual DbSet<AuditLog> ReviewAuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
