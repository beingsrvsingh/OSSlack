using System.Collections.Immutable;
using System.Security.Claims;
using Azure.Core;
using Identity.Application.Contracts;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.ChangePassword;
using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using JwtTokenAuthentication.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Common.Services.Interfaces;
using UAParser;
using Utilities.Services;

namespace Identity.Infrastructure.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITokenService tokenService;
        private readonly IJwtService jwtService;
        private readonly ISecurityService securityService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggerService loggerService;

        public IdentityService(
            UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService, SignInManager<ApplicationUser> _signInManager, ITokenService tokenService,
            IJwtService jwtService, ISecurityService securityService, IUnitOfWork unitOfWork, ILoggerService loggerService)
        {
            _userManager = userManager;
            this._signInManager = _signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            this.tokenService = tokenService;
            this.jwtService = jwtService;
            this.securityService = securityService;
            this.unitOfWork = unitOfWork;
            this.loggerService = loggerService;
        }

        public async Task<ApplicationUser?> GetUserNameAsync(string userId)
        {
            return await _userManager.Users.FirstAsync(u => u.Id == userId);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await this._userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser?> GetUserByPhoneNumberAsync(int phoneNumber)
        {
            string mobileNumber = phoneNumber.ToString();
            return await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobileNumber);
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await this._userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetUserAsync(string userId)
        {
            return await this._userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserEmailCommand request, CancellationToken cancellationToken = default)
        {
            var applicationUser = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Email
            };

            return await _userManager.CreateAsync(applicationUser, request.Password);
        }

        public async Task<String?> CreateUserPhoneAsync(CreateUserPhoneCommand request, CancellationToken cancellationToken = default)
        {
            var applicationUser = new ApplicationUser()
            {
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = true
            };

            this.unitOfWork.ApplicationUserRepository.AddAsync(applicationUser);

            await this.unitOfWork.SaveChangesAsync();

            var users = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
            return users?.Id;
        }

        public async Task CreateUserRoleAsync(String userId, string roleName, CancellationToken cancellationToken = default)
        {
            var applicationUser = await this._userManager.FindByIdAsync(userId);

            if (applicationUser is not null )
            {
                await _userManager.AddToRoleAsync(applicationUser, roleName);
            }
        }

        public async Task CreateSigningKeyAsync(string userId, CancellationToken cancellationToken = default) {
            await this.jwtService.CreateSigningKeyAsync(userId);
        }

        public async Task<SignInResult?> LoginAsync(LoginUserEmailCommand request)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true            
            var applicationUser = await this.GetUserByEmailAsync(request.Email);
            return await _signInManager.CheckPasswordSignInAsync(applicationUser!, request.Password, lockoutOnFailure: true);
        }

        public async Task<ApplicationUser?> LoginAsync(LoginUserPhoneCommand request)
        {
            return await this.GetUserByPhoneNumberAsync(request.PhoneNumber);
        }

        public async Task<AuthenticateResponse?> GenerateTokenAsync(string userId)
        {
            var authenticationToken = await tokenService.GenerateAccessToken(userId);

            var refreshToken = await jwtService.GenerateRefreshToken(userId, securityService.GetIpAddress);

            var token = refreshToken.Adapt<AspNetUserRefreshToken>();
            token.UserId = userId;
            await tokenService.SaveRefreshTokenAsync(token);

            if (authenticationToken != null)
            {
                return new AuthenticateResponse(authenticationToken.Id, authenticationToken.AccessToken, refreshToken.Token, authenticationToken.ExpiresIn);
            }
            this.loggerService.LogInfo("Phone number authentication token is null.");
            return null;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return false;

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }        

        public async Task<List<Claim>> GetClaims(EmailDto email)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, email.Email)
            };

            var user = await _userManager.FindByEmailAsync(email.Email);

            if (user is null)
            {
                return new List<Claim>();
            }

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        public async Task ChangePasswordAsync(ApplicationUser user, ChangePasswordCommand request, CancellationToken cancellationToken = default)
        {
            await this._userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        }

        public async Task<string> ForgotPasswordAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            return await this._userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task SetPasswordAsync(ApplicationUser user, SetPasswordCommand request, CancellationToken cancellationToken = default)
        {
            await this._userManager.ResetPasswordAsync(user, "", request.Password);
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<int> AddUserDevicesAsync(String userId, CancellationToken cancellationToken = default)
        {
            var uaParser = Parser.GetDefault();
            ClientInfo client = uaParser.Parse(securityService.GetUserAget);

            var aspNetUserDevice = new AspNetUserDevice() { UserId = userId, IpAddress = securityService.GetIpAddress, DeviceName = client.Device.Family, Browser = client.UA.Family, OS = client.OS.Family };

            this.unitOfWork.UserDevicesRepository.AddAsync(aspNetUserDevice);
            return await this.unitOfWork.SaveChangesAsync();
        }
    }
}
