using JwtTokenAuthentication.Application.Contracts;
using JwtTokenAuthentication.Domain.Entities;
using System.Security.Claims;

namespace JwtTokenAuthentication.Application.Interfaces
{
    public interface IJwtService
    {
        Task<AspNetUserSecurityToken?> GetSecurityTokenAsync(string userId);        
        Task<string> GenerateAccessTokenAsync(List<Claim> user);
        Task<RefreshTokenResponse> GenerateRefreshToken(string userId, string ipAddress);
        Task CreateSigningKeyAsync(string userId, CancellationToken cancellationToken = default);

        string GetClaims(List<Claim> claims, string claimTypes, CancellationToken cancellationToken = default);

    }
}
