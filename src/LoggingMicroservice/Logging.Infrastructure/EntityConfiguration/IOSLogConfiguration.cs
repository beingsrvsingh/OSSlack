using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logging.Infrastructure.EntityConfiguration
{
    public class IOSLogConfiguration : BaseLogEntityConfiguration<IOSLog>
    {
        public override void Configure(EntityTypeBuilder<IOSLog> builder)
        {
            base.Configure(builder);
            builder.ToTable("ios_logs");

            builder.Property(x => x.IosOsVersion).HasColumnName("ios_os_version");
            builder.Property(x => x.IosDeviceModel).HasColumnName("ios_device_model");
            builder.Property(x => x.IosBuildNumber).HasColumnName("ios_build_number");
            builder.Property(x => x.IsJailbroken).HasColumnName("is_jailbroken");
            builder.Property(x => x.NetworkType).HasColumnName("network_type");
            builder.Property(x => x.BatteryState).HasColumnName("battery_state");
            builder.Property(x => x.BatteryLevel).HasColumnName("battery_level");
            builder.Property(x => x.Orientation).HasColumnName("orientation");
            builder.Property(x => x.StorageFreeSpace).HasColumnName("storage_free_space");
            builder.Property(x => x.ScreenResolution).HasColumnName("screen_resolution");
            builder.Property(x => x.MemoryUsage).HasColumnName("memory_usage");
            builder.Property(x => x.CpuInfo).HasColumnName("cpu_info");

            builder.HasIndex(x => x.IosOsVersion).HasDatabaseName("idx_ios_os_version");

        }
    }

}
