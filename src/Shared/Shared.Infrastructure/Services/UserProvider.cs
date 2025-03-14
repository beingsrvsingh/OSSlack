using Microsoft.AspNetCore.Http;
using Shared.Application.Common.Services.Interfaces;
using System.Security.Claims;

namespace Shared.Infrastructure.Services
{
    public class UserProvider(IHttpContextAccessor httpContextAccessor) : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue("Id")!;

        public string UserName => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name)!;

        public string Role => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role)!;

        public string Email => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name)!;
    }
}
