using System.Security.Claims;
using Identity.Application.Contracts;
using Identity.Application.Interfaces;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using JwtTokenAuthentication.Constants;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Utilities;

#nullable enable

namespace Identity.Infrastructure.Services.Identity
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly ILogger<TokenService>? _logger;

        public TokenService(
            ILogger<TokenService> logger,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IJwtService jwtService)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }
        public async Task<AuthenticateResponse?> GenerateAccessToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger?.LogWarning("Access token generation failed: user not found (ID: {UserId})", userId);
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = BuildClaims(user, roles);

            var tokenString = await _jwtService.GenerateAccessTokenAsync(claims);

            var jwtOptions = Helper.LoadAppSettings();
            int tokenLifetimeMinutes = jwtOptions.GetSection("JwtSettings").GetValue<int>("TokenLifetimeMinutes");
            if (tokenLifetimeMinutes <= 0)
            {
                tokenLifetimeMinutes = 60;
            }

            _logger?.LogInformation("Access token generated for user {UserId}", userId);

            return Tokens.GenerateJwt(user.Id, tokenString, string.Empty, tokenLifetimeMinutes);
        }

        public async Task<AuthenticateResponse?> GenerateRefreshTokenAsync(string userId, string token, string ipAddress)
        {
            var existingToken = await GetRefreshToken(token, userId);
            if (existingToken == null)
            {
                _logger?.LogWarning("Refresh token not found for user {UserId}", userId);
                return null;
            }

            var jwtToken = await GenerateAccessToken(userId);
            if (jwtToken == null)
            {
                _logger?.LogError("Failed to generate new access token during refresh for user {UserId}", userId);
                return null;
            }

            var newRefreshToken = await _jwtService.GenerateRefreshToken(userId, ipAddress);
            var refreshTokenEntity = newRefreshToken.Adapt<AspNetUserRefreshToken>();
            refreshTokenEntity.UserId = userId;

            await _unitOfWork.RefreshTokenRepository.UpdateAsync(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();

            _logger?.LogInformation("Refresh token rotated for user {UserId}", userId);

            return new AuthenticateResponse(userId, jwtToken.AccessToken, newRefreshToken.Token, jwtToken.ExpiresIn);
        }

        public async Task<AspNetUserRefreshToken?> GetRefreshToken(string token, string userId)
        {
            return await _unitOfWork.RefreshTokenRepository.FirstOrDefaultAsync(
                t => t.Token == token && t.UserId == userId
            );
        }

        public async Task<bool> SaveRefreshTokenAsync(AspNetUserRefreshToken refreshToken)
        {
            try
            {
                _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
                await _unitOfWork.SaveChangesAsync();

                _logger?.LogInformation("Refresh token saved for user {UserId}", refreshToken.UserId);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to save refresh token for user {UserId}", refreshToken.UserId);
                return false;
            }
        }

        public async Task<bool> RevokeTokenAsync(AspNetUserRefreshToken refreshToken)
        {
            try
            {
                await _unitOfWork.RefreshTokenRepository.DeleteAsync(refreshToken);
                await _unitOfWork.SaveChangesAsync();

                _logger?.LogInformation("Refresh token revoked for user {UserId}", refreshToken.UserId);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to revoke refresh token for user {UserId}", refreshToken.UserId);
                return false;
            }
        }

        private static List<Claim> BuildClaims(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtConstant.JWT_TOKEN_USERID_KEYS, user.Id),
                new Claim(JwtConstant.JWT_TOKEN_ROLE_KEYS, roles.FirstOrDefault()?.ToLower() ?? "customer"),
                new Claim(JwtConstant.JWT_TOKEN_SCOPE_KEYS, JwtConstant.JWT_TOKEN_SCOPE)
            };

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                claims.Add(new Claim(JwtConstant.JWT_TOKEN_PHONENUMBER_KEYS, user.PhoneNumber));
            }

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                claims.Add(new Claim(JwtConstant.JWT_TOKEN_USERNAME_KEYS, user.Email));
            }

            return claims;
        }
    }
}
