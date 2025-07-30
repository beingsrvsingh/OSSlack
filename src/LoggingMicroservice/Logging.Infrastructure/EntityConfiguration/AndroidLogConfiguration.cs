using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logging.Infrastructure.EntityConfiguration
{
    public class AndroidLogConfiguration : BaseLogEntityConfiguration<AndroidLog>
    {
        public override void Configure(EntityTypeBuilder<AndroidLog> builder)
        {
            base.Configure(builder);
            builder.ToTable("android_logs");

            builder.Property(x => x.AndroidOsVersion).HasColumnName("android_os_version");
            builder.Property(x => x.AndroidDeviceModel).HasColumnName("android_device_model");
            builder.Property(x => x.AndroidManufacturer).HasColumnName("android_manufacturer");
            builder.Property(x => x.AndroidBuildId).HasColumnName("android_build_id");
            builder.Property(x => x.IsRooted).HasColumnName("is_rooted");
            builder.Property(x => x.NetworkType).HasColumnName("network_type");
            builder.Property(x => x.BatteryStatus).HasColumnName("battery_status");
            builder.Property(x => x.BatteryLevel).HasColumnName("battery_level");
            builder.Property(x => x.Orientation).HasColumnName("orientation");
            builder.Property(x => x.StorageFreeSpace).HasColumnName("storage_free_space");
            builder.Property(x => x.ScreenResolution).HasColumnName("screen_resolution");
            builder.Property(x => x.MemoryUsage).HasColumnName("memory_usage");
            builder.Property(x => x.CpuInfo).HasColumnName("cpu_info");

            builder.HasIndex(x => x.AndroidOsVersion).HasDatabaseName("idx_android_os_version");

        }
    }

}

