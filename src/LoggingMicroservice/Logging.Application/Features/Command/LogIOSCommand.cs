
using MediatR;
using Shared.Utilities.Response;

namespace Logging.Application.Features.Command
{
    public class LogIOSCommand: IRequest<Result>
    {
        public string Message { get; set; } = string.Empty;
        public string LogLevel { get; set; } = string.Empty;
        public string? IosOsVersion { get; set; }
        public string? IosDeviceModel { get; set; }
        public string? ExceptionMessage { get; set; }
    }
}