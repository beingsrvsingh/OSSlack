using JwtTokenAuthentication.Application;
using JwtTokenAuthentication.Application.Contracts;
using JwtTokenAuthentication.Application.Interfaces;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities;
using Utilities.Cryptography;
using Utilities.Interfaces;

namespace JwtTokenAuthentication.Infrastructure.Services
{
    public class JwtService(ITokenDbContext context, IRegistryService registryService) : IJwtService
    {
        private readonly ITokenDbContext _context = context;
        private readonly IRegistryService _registryService = registryService;

        public async Task CreateSigningKeyAsync(string userId, CancellationToken cancellationToken)
        {
            AspNetUserSecurityToken userSecurityToken = new()
            {
                UserId = userId,
                SecurityKey = SecurityTokenExtensions.GenerateSecurityKey(userId)
            };

            await _context.AspNetUserSecurityTokens.AddAsync(userSecurityToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> GenerateAccessTokenAsync(List<Claim> claims)
        {
            string userId = GetClaims(claims, JwtConstant.JWT_TOKEN_USERID_KEYS);

            var key = await GetSecurityTokenAsync(userId);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key?.SecurityKey!));

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
            if (!string.IsNullOrEmpty(key?.SecurityKey))
            {
                refreshToken = new RefreshTokenResponse
                {
                    Token = Convert.ToBase64String(Utitlities.GenerateRandomNumber(Cryptography.DecryptString(key.SecurityKey))),
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

        public async Task<AspNetUserSecurityToken?> GetSecurityTokenAsync(string userId)
        {
            var key = await _context.AspNetUserSecurityTokens.FirstOrDefaultAsync(x => x.UserId == userId)!;

            if (string.IsNullOrEmpty(key?.SecurityKey))
                return null;

            return key;
        }
    }
}
