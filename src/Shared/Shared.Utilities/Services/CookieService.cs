using Microsoft.AspNetCore.Http;
using Shared.Utilities.Interfaces;

namespace Shared.Utilities.Services
{
    public class CookieService(IHttpContextAccessor contextAccessor) : ICookieService
    {
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        public string GetCookie(string key)
        {
            return _contextAccessor?.HttpContext?.Request.Cookies[key]!;
        }

        public void SetCookie(string key, DateTime expiresIn)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expiresIn
            };
            _contextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", key, cookieOptions);
        }

        public void RemoveAndSetCookie(string key, DateTime expiresIn)
        {
            // append cookie with refresh token to the http response
            string cookie = GetCookie("refreshToken");

            if (!string.IsNullOrEmpty(cookie))
                DeleteCookie("refreshToken");

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expiresIn
            };
            _contextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", key, cookieOptions);
        }

        public void DeleteCookie(string key)
        {
            _contextAccessor?.HttpContext?.Response.Cookies.Delete(key);

        }
    }
}
