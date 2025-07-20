using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EntityConfigurations;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class AuditLogEntityTypeConfiguration : BaseAuditLogConfiguration<AuditLog>
    {
        public override void Configure(EntityTypeBuilder<AuditLog> entity)
        {
            base.Configure(entity);

            // Optional: Add microservice-specific config here
            entity.HasOne(e => e.User)
                  .WithMany(e => e.AspNetUserAudits)
                  .HasForeignKey(e => e.UserId);
        }
    }
}
