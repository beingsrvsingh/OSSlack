using Identity.Application.Contracts;
using Identity.Application.Interfaces;
using JwtTokenAuthentication.Constants;
using Microsoft.IdentityModel.Tokens;
using Shared.Contracts.Interfaces;
using Shared.Utilities;
using Shared.Utilities.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IHttpRequestService securityService;
        private readonly IKeyValueProvider _provider;
        private readonly string jwtSigningKey;

        public JwtService(IHttpRequestService securityService, IKeyValueProvider provider)
        {
            this.securityService = securityService;
            this._provider = provider;
            var jwtSigningKey = Configuration.GetValue<string>("Secrets", "jwtSigningKey");
            this.jwtSigningKey = jwtSigningKey;
        }

        public Task<string> GenerateAccessTokenAsync(List<Claim> claims)
        {
            string secretSigningValue = _provider.GetValue("platform", jwtSigningKey);

            // Create SymmetricSecurityKey
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretSigningValue));

            var signinCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JwtConstant.JWT_TOKEN_ISSUER,
                audience: JwtConstant.JWT_TOKEN_AUDIENCE,
                claims: claims,
                expires: JwtConstant.JWT_TOKEN_EXPIRATION,
                signingCredentials: signinCredentials
            );

            // Generate Access Token
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(accessToken);
        }

        public Task<RefreshTokenResponse> GenerateRefreshToken(string userId, string ipAddress)
        {
            // generate token that is valid for 7 days            
            var refreshTokenLifeTimeInDays = Configuration.GetValue<int>("JwtSettings", "RefreshTokenLifetimeInDays");
            if (refreshTokenLifeTimeInDays <= 0)
            {
                refreshTokenLifeTimeInDays = 7;
            }

            var refreshToken = new RefreshTokenResponse();

            refreshToken = new RefreshTokenResponse
            {
                Token = Utils.GenerateSecureToken(),
                Expires = DateTime.UtcNow.AddDays(refreshTokenLifeTimeInDays),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
            return Task.FromResult(refreshToken);
        }

        public string GetClaims(List<Claim> claims, string claimTypes, CancellationToken cancellationToken = default)
        {
            return claims.First(c => JwtConstant.JWT_TOKEN_USERID_KEYS == claimTypes).Value;
        }
    }
}
