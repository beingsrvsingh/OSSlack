using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logging.Infrastructure.EntityConfiguration
{
    public class AppsLogEntityTypeConfiguration : IEntityTypeConfiguration<AppsLog>
    {
        public void Configure(EntityTypeBuilder<AppsLog> entity)
        {
            entity.HasIndex(e => e.UserId);
            entity.Property(e => e.IpAddress).HasColumnType("varchar").HasMaxLength(20);
            entity.Property(e => e.Level).HasColumnType("varchar").HasMaxLength(10);
            entity.Property(e => e.Message).HasColumnType("varchar").HasMaxLength(500);
            entity.Property(e => e.Exception).HasColumnType("varchar").HasMaxLength(2000);
            entity.Property(e => e.Logger).HasColumnType("varchar").HasMaxLength(2000);
            entity.Property(e => e.Callsite).HasColumnType("varchar").HasMaxLength(2000);
        }
    }
}
