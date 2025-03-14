using System.ComponentModel.DataAnnotations;

namespace Logging.Domain.Entities
{
    public partial class AppsLog : IEntity
    {
        [Key]
        public string UserId { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public DateTime Logged { get; set; }
        public string? Level { get; set; }
        public string Message { get; set; } = null!;
        public string? Exception { get; set; }
        public string? Callsite { get; set; }
        public string? Logger { get; set; }
    }
}
