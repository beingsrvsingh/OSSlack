using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class ReviewAuditLogEntityTypeConfiguration : IEntityTypeConfiguration<ReviewAuditLog>
    {
        public void Configure(EntityTypeBuilder<ReviewAuditLog> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId).IsClustered(false);
            entity.HasIndex(e => e.TableId).IsClustered(false);
            entity.HasIndex(e => e.TableName).IsClustered(false);
            entity.Property(e => e.TableId).HasMaxLength(25);
            entity.Property(e => e.Action).HasMaxLength(10);
            entity.Property(e => e.CreatedOn);
            entity.Property(e => e.TableName).HasMaxLength(50);
        }
    }
}
