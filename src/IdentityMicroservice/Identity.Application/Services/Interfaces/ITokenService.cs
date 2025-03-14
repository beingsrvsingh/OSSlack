using Identity.Application.Contracts;
using Identity.Domain.Entities;

namespace Identity.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<AuthenticateResponse?> GenerateAccessToken(string userId);

        Task<AuthenticateResponse?> GenerateRefreshTokenAsync(string userId, string token, string ipAddress);

        Task SaveRefreshTokenAsync(AspNetUserRefreshToken refreshToken);

        Task RevokeTokenAsync(AspNetUserRefreshToken aspNetUserRefreshToken);

        Task<AspNetUserRefreshToken?> GetRefreshToken(string token, string userId);
    }
}
