using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Data.EntityConfigurations
{
    internal class AspNetUserDevicesEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserDevice>
    {
        public void Configure(EntityTypeBuilder<AspNetUserDevice> entity)
        {
            entity.Property(e => e.UserId).HasMaxLength(50);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.DeviceName).HasMaxLength(100);
            entity.Property(e => e.Browser).HasMaxLength(100);
            entity.Property(e => e.OS).HasMaxLength(20);
        }
    }
}
