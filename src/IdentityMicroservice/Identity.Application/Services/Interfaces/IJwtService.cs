using Identity.Application.Contracts;
using System.Security.Claims;

namespace Identity.Application.Interfaces
{
    public interface IJwtService
    {     
        Task<string> GenerateAccessTokenAsync(List<Claim> user);
        Task<RefreshTokenResponse> GenerateRefreshToken(string userId, string ipAddress);
        string GetClaims(List<Claim> claims, string claimTypes, CancellationToken cancellationToken = default);

    }
}
