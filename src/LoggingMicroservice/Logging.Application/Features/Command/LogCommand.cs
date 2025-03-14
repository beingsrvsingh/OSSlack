using MediatR;

namespace Logging.Application.Features.Command
{
    public class LogCommand : IRequest
    {
        public required string UserId { get; set; }
        public string IpAddress { get; set; } = null!;
        public DateTime Logged { get; set; }
        public string? Level { get; set; }
        public string Message { get; set; } = null!;
        public string? Exception { get; set; }
        public string? Callsite { get; set; }
        public string? Logger { get; set; }
    }
}
