using System.ComponentModel.DataAnnotations.Schema;

namespace Logging.Domain.Entities
{
    [Table("AndroidLogs")]
    public class AndroidLog : BaseLogEntity
    {
        public string? AndroidOsVersion { get; set; }
        public string? AndroidDeviceModel { get; set; }
        public string? AndroidManufacturer { get; set; }
        public string? AndroidBuildId { get; set; }
        public bool? IsRooted { get; set; }
        public string? NetworkType { get; set; }
        public string? BatteryStatus { get; set; }
        public float? BatteryLevel { get; set; }
        public string? Orientation { get; set; }
        public string? StorageFreeSpace { get; set; }
        public string? ScreenResolution { get; set; }
        public string? MemoryUsage { get; set; }
        public string? CpuInfo { get; set; }
    }
}
