using Microsoft.AspNetCore.Http;

namespace Utilities.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public SecurityService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetIpAddress
        {
            get
            {
                // get source ip address for the current request
                if (_contextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                    return _contextAccessor.HttpContext.Request.Headers["X-Forwarded-For"]!;
                else
                    return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        public string GetUserAget => _contextAccessor.HttpContext != null ? _contextAccessor.HttpContext?.Request?.Headers["User-Agent"]! : "Autonomous";
    }
}
