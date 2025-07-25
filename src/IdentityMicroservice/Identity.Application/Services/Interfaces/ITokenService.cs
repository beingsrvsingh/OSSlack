using Identity.Application.Contracts;
using Identity.Domain.Entities;

namespace Identity.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<AuthenticateResponse?> GenerateAccessToken(string userId);

        Task<AuthenticateResponse?> GenerateRefreshTokenAsync(string userId, string token, string ipAddress);

        Task<bool> SaveRefreshTokenAsync(AspNetUserRefreshToken refreshToken);

        Task<bool> RevokeTokenAsync(AspNetUserRefreshToken aspNetUserRefreshToken);

        Task<AspNetUserRefreshToken?> GetRefreshToken(string token, string userId);
    }
}
