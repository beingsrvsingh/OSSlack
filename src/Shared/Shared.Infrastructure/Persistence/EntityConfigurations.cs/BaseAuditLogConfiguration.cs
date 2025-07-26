using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Entities;

namespace Shared.Infrastructure.EntityConfigurations;

public class BaseAuditLogConfiguration<TAuditLog> : IEntityTypeConfiguration<TAuditLog>
    where TAuditLog : BaseAuditLog
{
    public virtual void Configure(EntityTypeBuilder<TAuditLog> entity)
    {
        entity.ToTable("audit_log");

        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.UserId).IsClustered(false);
        entity.HasIndex(e => e.TableId).IsClustered(false);
        entity.HasIndex(e => e.TableName).IsClustered(false);

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.UserId).HasColumnName("user_id");
        entity.Property(e => e.TableId).HasMaxLength(50).HasColumnName("table_id");
        entity.Property(e => e.OldValues).HasColumnName("old_values");
        entity.Property(e => e.NewValues).HasColumnName("new_values");
        entity.Property(e => e.IpAddress).HasColumnName("ip_address");
        entity.Property(e => e.Action).HasMaxLength(10).HasColumnName("action");
        entity.Property(e => e.CreatedOn).HasColumnName("created_on");
        entity.Property(e => e.TableName).HasMaxLength(50).HasColumnName("table_name");
    }
}
