using Identity.Application.Contracts;
using Identity.Application.Interfaces;
using Identity.Domain.Core.Repository;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities;
using Utilities.Cryptography;
using Utilities.Services;

namespace Identity.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IUserSigningKeyRepository userSigningKeyRepository;
        private readonly ISecurityService securityService;

        public JwtService(ISecurityService securityService, IUserSigningKeyRepository userSigningKeyRepository)
        {
            this.userSigningKeyRepository = userSigningKeyRepository;
            this.securityService = securityService;
        }
        public async Task CreateSigningKeyAsync(string userId, CancellationToken cancellationToken)
        {
            AspNetUserSigningKey signingKey = new()
            {
                UserId = userId,
                SigningHash = Cryptography.GenerateHash(userId),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                IsRevoked = false,
                CreatedByIp = securityService.GetIpAddress,
                UserAgent = securityService.GetUserAget,
                EncryptedSigningKey = Cryptography.EncryptString(userId)
            };

            userSigningKeyRepository.AddAsync(signingKey);
            await userSigningKeyRepository.SaveChangesAsync();
        }

        public async Task<string> GenerateAccessTokenAsync(List<Claim> claims)
        {
            string userId = GetClaims(claims, JwtConstant.JWT_TOKEN_USERID_KEYS);

            var key = await GetSecurityTokenAsync(userId);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key?.EncryptedSigningKey!));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JwtConstant.JWT_TOKEN_ISSUER,
                audience: JwtConstant.JWT_TOKEN_AUDIENCE,
                claims: claims,
                expires: JwtConstant.JWT_TOKEN_EXPIRATION,
                signingCredentials: signinCredentials
            );

            // Generate Access Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<RefreshTokenResponse> GenerateRefreshToken(string userId, string ipAddress)
        {
            // generate token that is valid for 7 days
            var key = await GetSecurityTokenAsync(userId);
            var refreshToken = new RefreshTokenResponse();
            if (!string.IsNullOrEmpty(key?.EncryptedSigningKey))
            {
                refreshToken = new RefreshTokenResponse
                {
                    Token = Convert.ToBase64String(Utitlities.GenerateRandomNumber(Cryptography.DecryptString(key.EncryptedSigningKey))),
                    Expires = JwtConstant.JWT_REFRESH_TOKEN_EXPIRATION,
                    Created = DateTime.Now,
                    CreatedByIp = ipAddress
                };
            }
            return refreshToken;
        }

        public string GetClaims(List<Claim> claims, string claimTypes, CancellationToken cancellationToken = default)
        {
            return claims.First(c => JwtConstant.JWT_TOKEN_USERID_KEYS == claimTypes).Value;
        }

        public async Task<AspNetUserSigningKey?> GetSecurityTokenAsync(string userId)
        {
            var key = await userSigningKeyRepository.FirstOrDefaultAsync(x => x.UserId == userId)!;

            if (string.IsNullOrEmpty(key?.EncryptedSigningKey))
                return null;

            return key;
        }
    }
}
