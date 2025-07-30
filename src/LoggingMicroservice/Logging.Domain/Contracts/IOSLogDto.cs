
namespace Logging.Domain.Contracts
{
    public class IOSLogDto
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; } = string.Empty;
        public string LogLevel { get; set; } = string.Empty;
        public string? IosOsVersion { get; set; }
        public string? IosDeviceModel { get; set; }
        public string? ExceptionMessage { get; set; }
    }

}