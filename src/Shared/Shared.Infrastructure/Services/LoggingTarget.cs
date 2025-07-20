using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;
using NLog.Targets;
using Shared.Application.Common.Services.Interfaces;
using Shared.Domain.Entities;
using System.Dynamic;
using System.Security.Claims;
using System.Text.Json;
using Utilities.Services;

namespace Shared.Utilities.Services
{
    [Target("LoggingAPI")]
    public sealed class LoggingTarget : TargetWithLayout
    {
        [RequiredParameter]
        public string Host { get; set; }

        private IHttpContextAccessor context = new HttpContextAccessor();
        private string? userId => context.HttpContext?.User?.FindFirstValue("Id")!;
        private ILoggingApiClient loggingClient => context.HttpContext!.RequestServices.GetRequiredService<ILoggingApiClient>();
        private ISecurityService securityService => context.HttpContext!.RequestServices.GetRequiredService<ISecurityService>();

        public LoggingTarget()
        {
            Host = $"Logging.API-{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";
        }

        protected async override void Write(LogEventInfo logEvent)
        {
            //await SendTheMessageToRemoteHost(this.Host, logEvent);
        }

        private async Task SendTheMessageToRemoteHost(string host, LogEventInfo logEvent)
        {
            //Post
            BaseLog log = new BaseLog()
            {

                UserId = userId is null ? string.Empty : userId,
                Level = logEvent.Level.Name,
                IpAddress = securityService.GetIpAddress,
                Logged = DateTime.UtcNow,
                Message = logEvent.Message,
                Exception = Convert.ToString(logEvent.Exception),
                Callsite = Convert.ToString(logEvent.Exception?.StackTrace),
                Logger = logEvent.LoggerName,
            };

            await loggingClient.AddLogAsync(log);
        }

    }
}
