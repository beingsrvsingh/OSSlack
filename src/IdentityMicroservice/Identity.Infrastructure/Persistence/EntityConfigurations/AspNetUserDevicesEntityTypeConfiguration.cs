using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Data.EntityConfigurations
{
    internal class AspNetUserDevicesEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserDevice>
    {
        public void Configure(EntityTypeBuilder<AspNetUserDevice> entity)
        {
            // Table name (optional, for naming consistency)
            entity.ToTable("aspnet_user_device");

            // Primary key
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");

            // Properties
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            entity.Property(e => e.IpAddress)
                .IsRequired()
                .HasMaxLength(45) // Max for IPv6
                .HasColumnName("ip_address");

            entity.Property(e => e.DeviceName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("device_name");

            entity.Property(e => e.Browser)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("browser");

            entity.Property(e => e.OS)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("os");

            entity.Property(e => e.LastAccessed)
                .HasColumnName("last_accessed");

            entity.Property(e => e.IsCurrent)
                .HasColumnName("is_current");

            entity.Property(e => e.IsTrusted)
                .HasColumnName("is_trusted");

            // Foreign key relationship (optional if User is not tracked in the same context)
            entity.HasOne(e => e.User)
                  .WithMany(e => e.AspNetUserDevices)
                  .HasForeignKey(e => e.UserId);
        }
    }
}
