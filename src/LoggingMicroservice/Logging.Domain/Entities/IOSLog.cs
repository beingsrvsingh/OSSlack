using System.ComponentModel.DataAnnotations.Schema;

namespace Logging.Domain.Entities
{
    [Table("IOSLogs")]
    public class IOSLog : BaseLogEntity
    {
        public string? IosOsVersion { get; set; }
        public string? IosDeviceModel { get; set; }
        public string? IosBuildNumber { get; set; }
        public bool? IsJailbroken { get; set; }
        public string? NetworkType { get; set; }
        public string? BatteryState { get; set; }
        public float? BatteryLevel { get; set; }
        public string? Orientation { get; set; }
        public string? StorageFreeSpace { get; set; }
        public string? ScreenResolution { get; set; }
        public string? MemoryUsage { get; set; }
        public string? CpuInfo { get; set; }
    }
}