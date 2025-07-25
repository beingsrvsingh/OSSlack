using Identity.Application.Contracts;
using Identity.Application.Interfaces;
using JwtTokenAuthentication.Constants;
using Microsoft.IdentityModel.Tokens;
using Shared.Contracts.Interfaces;
using Shared.Utilities;
using Shared.Utilities.Interfaces;
using Shared.Utilities.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IHttpRequestService securityService;
        private readonly ISigningKeyProvider signingKeyProvider;

        public JwtService(IHttpRequestService securityService, ISigningKeyProvider signingKeyProvider)
        {
            this.securityService = securityService;
            this.signingKeyProvider = signingKeyProvider;
        }

        public Task<string> GenerateAccessTokenAsync(List<Claim> claims)
        {
            string rawKey = RetrieveSigningKey();

            // Create SymmetricSecurityKey
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(rawKey));

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

        public async Task<RefreshTokenResponse> GenerateRefreshToken(string userId, string ipAddress)
        {
            // generate token that is valid for 7 days
            var refreshToken = new RefreshTokenResponse();
            string rawKey = RetrieveSigningKey();
            if (!string.IsNullOrEmpty(rawKey))
            {
                refreshToken = new RefreshTokenResponse
                {
                    Token = Utils.GenerateSecureToken(),
                    Expires = JwtConstant.JWT_REFRESH_TOKEN_EXPIRATION,
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
            return refreshToken;
        }

        public string GetClaims(List<Claim> claims, string claimTypes, CancellationToken cancellationToken = default)
        {
            return claims.First(c => JwtConstant.JWT_TOKEN_USERID_KEYS == claimTypes).Value;
        }
        
        private string RetrieveSigningKey()
        {
            var response = signingKeyProvider.GetSigningKey();

            if (string.IsNullOrWhiteSpace(response))
            {
                throw new InvalidOperationException("Signing key response from SecretManager is null or empty.");
            }

            // Deserialize JSON into Result
            var keyResult = JsonSerializerWrapper.Deserialize<Result>(response)
                ?? throw new InvalidOperationException("Failed to deserialize signing key result.");

            // Extract the key safely
            var rawKey = keyResult.Data?.ToString();

            if (string.IsNullOrWhiteSpace(rawKey))
            {
                throw new InvalidOperationException("SecretManager returned no usable signing key in response.");
            }

            return rawKey;
        }
    }
}
