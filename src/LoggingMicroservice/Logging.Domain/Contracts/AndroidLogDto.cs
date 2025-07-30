
namespace Logging.Domain.Contracts
{
    public class AndroidLogDto
    {
        // When the log was recorded
        public DateTime Timestamp { get; set; }
        // Log message content
        public string Message { get; set; } = string.Empty;
        // Severity level (Info, Warning, Error, etc.)
        public string LogLevel { get; set; } = string.Empty;
        // OS version of the Android device
        public string? AndroidOsVersion { get; set; }
        // Device model (e.g., Pixel 6, Samsung S21)
        public string? AndroidDeviceModel { get; set; }
        // Top-level exception message (if any)
        public string? ExceptionMessage { get; set; }
    }

}