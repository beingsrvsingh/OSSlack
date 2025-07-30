using System.ComponentModel.DataAnnotations.Schema;

namespace Logging.Domain.Entities
{
    [Table("WebServiceLogs")]
    public class WebServiceLog : BaseLogEntity
    {
        public string? IpAddress { get; set; }
        public string? Endpoint { get; set; }
        public int? HttpStatusCode { get; set; }
        public string? HttpMethod { get; set; }
        public string? Referrer { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }
        public string? UserAgent { get; set; }
        public long? ResponseTimeMs { get; set; }
        public string? ExceptionDetails { get; set; }
    }
}
