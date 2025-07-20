using System.Security.Claims;
using Identity.Application.Contracts;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using JwtTokenAuthentication.Application.Interfaces;
using JwtTokenAuthentication.Constants;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Common.Services.Interfaces;
using Shared.Utilities.Response;
using Utilities.Services;

#nullable enable

namespace Identity.Infrastructure.Services.Identity
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService jwtService;
        private readonly IUserProvider userProvider;

        public TokenService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IJwtService jwtService, IUserProvider userProvider)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.jwtService = jwtService;
            this.userProvider = userProvider;
        }

        public async Task<AuthenticateResponse?> GenerateAccessToken(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                // Generate Access Token
                var role = await userManager.GetRolesAsync(user);
                var tokenString = await jwtService.GenerateAccessTokenAsync(RequiredParamsForGenerateToken(user!, role));

                return Tokens.GenerateJwt(user!.Id, tokenString, string.Empty, (int)JwtConstant.JWT_TOKEN_EXPIRATION.Subtract(DateTime.Now).TotalSeconds);
            }
            return null;
        }

        public async Task<AuthenticateResponse?> GenerateRefreshTokenAsync(string userId, string token, string ipAddress)
        {
            // replace old refresh token with a new one (rotate token)

            var existingToken = await GetRefreshToken(token, userId);

            if (existingToken is null)
            {
                return null;
            }

            // generate new jwt
            var jwtToken = await GenerateAccessToken(userId);

            if (jwtToken is not null)
            {
                var newRefreshToken = await jwtService.GenerateRefreshToken(userId, ipAddress);
                var refreshToken = newRefreshToken.Adapt<AspNetUserRefreshToken>();
                refreshToken.UserId = userId;

                await unitOfWork.RefreshTokenRepository.UpdateAsync(refreshToken);
                await unitOfWork.SaveChangesAsync();

                return new AuthenticateResponse(userId, jwtToken.AccessToken, newRefreshToken.Token, jwtToken.ExpiresIn);
            }

            return null;
        }

        public async Task SaveRefreshTokenAsync(AspNetUserRefreshToken refreshToken)
        {
            unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task RevokeTokenAsync(AspNetUserRefreshToken aspNetUserRefreshToken)
        {
            await unitOfWork.RefreshTokenRepository.DeleteAsync(aspNetUserRefreshToken);
            await unitOfWork.SaveChangesAsync();
        }

        // helper methods 

        public async Task<AspNetUserRefreshToken?> GetRefreshToken(string token, string userId)
        {
            return await unitOfWork.RefreshTokenRepository.FirstOrDefaultAsync(u => u.Token == token && u.UserId == userId);
        }

        private static List<Claim> RequiredParamsForGenerateToken(ApplicationUser user, IList<string> role)
        {
            return AddClaims(user, role);
        }

        private static List<Claim> AddClaims(ApplicationUser user, IList<string> role)
        {
            
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtConstant.JWT_TOKEN_USERID_KEYS, user.Id));
            claims.Add(new Claim(JwtConstant.JWT_TOKEN_ROLE_KEYS, role.FirstOrDefault()?.ToLower() ?? "customer"));
            claims.Add(new Claim(JwtConstant.JWT_TOKEN_SCOPE_KEYS, JwtConstant.JWT_TOKEN_SCOPE));

            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                claims.Add(new Claim(JwtConstant.JWT_TOKEN_PHONENUMBER_KEYS, user.PhoneNumber));
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(JwtConstant.JWT_TOKEN_USERNAME_KEYS, user.Email));
            }

            return claims;
        }
    }
}
