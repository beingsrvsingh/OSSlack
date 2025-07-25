using Microsoft.AspNetCore.Http;
using Shared.Utilities.Interfaces;

namespace Shared.Utilities.Services
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public HttpRequestService(IHttpContextAccessor contextAccessor)
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

        public string GetUserAgent => _contextAccessor.HttpContext != null ? _contextAccessor.HttpContext?.Request?.Headers["User-Agent"]! : "Autonomous";
    }
}
