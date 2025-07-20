using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretManagement.Domain.Entities;
using Shared.Infrastructure.EntityConfigurations;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class AuditLogEntityTypeConfiguration : BaseAuditLogConfiguration<AuditLog>
    {
        public override void Configure(EntityTypeBuilder<AuditLog> entity)
        {
            base.Configure(entity);
        }
    }
}
