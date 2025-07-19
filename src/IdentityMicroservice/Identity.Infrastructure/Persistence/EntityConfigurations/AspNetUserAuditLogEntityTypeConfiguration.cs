using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class AspNetUserAuditLogEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserAuditLog>
    {
        public void Configure(EntityTypeBuilder<AspNetUserAuditLog> entity)
        {
            entity.ToTable("aspnet_user_audit_log");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId).IsClustered(false);
            entity.HasIndex(e => e.TableId).IsClustered(false);
            entity.HasIndex(e => e.TableName).IsClustered(false);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TableId).HasMaxLength(25).HasColumnName("table_id");
            entity.Property(e => e.Action).HasMaxLength(10).HasColumnName("action");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on");
            entity.Property(e => e.TableName).HasMaxLength(50).HasColumnName("table_name");
            entity.HasOne(e => e.User)
                 .WithMany(e => e.AspNetUserAudits)
                 .HasForeignKey(e => e.UserId);
        }
    }
}
