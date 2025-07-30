using MediatR;
using Shared.Utilities.Response;

namespace Logging.Application.Features.Command
{
    public class LogWebServiceCommand: IRequest<Result>
    {
        public string Message { get; set; } = string.Empty;
        public string LogLevel { get; set; } = string.Empty;
        public string? Endpoint { get; set; }
        public int? HttpStatusCode { get; set; }
        public string? ExceptionDetails { get; set; }
    }
}