
using MediatR;
using Shared.Utilities.Response;

namespace Logging.Application.Features.Command
{
    public class LogAndroidCommand : IRequest<Result>
    {
        public string Message { get; set; } = string.Empty;
        public string LogLevel { get; set; } = string.Empty;
        public string? AndroidOsVersion { get; set; }
        public string? AndroidDeviceModel { get; set; }
        public string? ExceptionMessage { get; set; }
    }
}
