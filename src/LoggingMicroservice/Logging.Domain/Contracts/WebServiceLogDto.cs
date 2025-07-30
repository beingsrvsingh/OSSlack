
namespace Logging.Domain.Contracts
{
    public class WebServiceLogDto
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; } = string.Empty;
        public string LogLevel { get; set; } = string.Empty;
        public string? Endpoint { get; set; }
        public int? HttpStatusCode { get; set; }
        public string? ExceptionDetails { get; set; }
    }
}