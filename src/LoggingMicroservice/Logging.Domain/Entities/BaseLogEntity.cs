using System.ComponentModel.DataAnnotations;

namespace Logging.Domain.Entities
{
    public abstract class BaseLogEntity : IEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required, MaxLength(10)]
        public string LogLevel { get; set; } = null!; // Always required

        [Required]
        public string Message { get; set; } = null!;  // Always required

        public string? UserId { get; set; }
        public string? SessionId { get; set; }
        public string? DeviceInfo { get; set; }
        public string? AppVersion { get; set; }
        public string? ExtraData { get; set; }

        // Exception-related
        public string? ExceptionMessage { get; set; }
        public string? ExceptionType { get; set; }
        public string? StackTrace { get; set; }
        public string? InnerExceptionMessage { get; set; }
        public string? InnerExceptionType { get; set; }
        public string? InnerStackTrace { get; set; }

        // Thread info
        public int? ThreadId { get; set; }
        public string? ThreadName { get; set; }

        // Source/debug info
        public string? Source { get; set; }
        public string? MethodName { get; set; }
        public int? LineNumber { get; set; }
        public string? FileName { get; set; }

        public bool CrashOccurred { get; set; } = false;

        public string? Breadcrumbs { get; set; }
        public string? MemoryUsageDetails { get; set; }
        public float? CpuUsage { get; set; }
        public string? NetworkStatus { get; set; }

        public string? Locale { get; set; }
        public string? AppState { get; set; }
    }
}