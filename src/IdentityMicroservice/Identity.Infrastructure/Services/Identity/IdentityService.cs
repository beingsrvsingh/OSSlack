using System.Security.Claims;
using Identity.Application.Contracts;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.ChangePassword;
using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Application.Interfaces;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Interfaces;
using UAParser;

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
        private readonly IHttpRequestService securityService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggerService<IdentityService> _logger;

        public IdentityService(ILoggerService<IdentityService> loggerService,
            UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService, SignInManager<ApplicationUser> _signInManager, ITokenService tokenService,
            IJwtService jwtService, IHttpRequestService securityService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            this._signInManager = _signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            this.tokenService = tokenService;
            this.jwtService = jwtService;
            this.securityService = securityService;
            this.unitOfWork = unitOfWork;
            this._logger = loggerService;
        }

        public async Task<ApplicationUser?> GetUserNameAsync(string userName)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
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

        public async Task<IdentityResult> CreateUserEmailAsync(CreateUserEmailCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                var applicationUser = new ApplicationUser()
                {
                    Email = request.Email,
                    UserName = request.Email,
                    FirebaseUid = request.FirebaseIdToken
                };

                return await _userManager.CreateAsync(applicationUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with email {Email}", request.Email);
                return IdentityResult.Failed(new IdentityError { Description = "Failed to create user due to an internal error." });
            }
        }

        public async Task<string?> CreateUserPhoneAsync(CreateUserPhoneCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                var applicationUser = new ApplicationUser()
                {
                    UserName = request.PhoneNumber,
                    PhoneNumber = request.PhoneNumber,
                    CountryCode = request.CountryCode,
                    PhoneNumberConfirmed = true,
                    FirebaseUid = request.FirebaseIdToken
                };

                this.unitOfWork.ApplicationUserRepository.AddAsync(applicationUser);
                await this.unitOfWork.SaveChangesAsync();

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
                return user?.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with phone number {PhoneNumber}", request.PhoneNumber);
                return null;
            }
        }

        public async Task<bool> CreateUserRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default)
        {
            try
            {
                var applicationUser = await _userManager.FindByIdAsync(userId);
                if (applicationUser is null) return false;

                var result = await _userManager.AddToRoleAsync(applicationUser, roleName);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding role {Role} to user {UserId}", roleName, userId);
                return false;
            }
        }

        public async Task<SignInResult?> LoginUserWithEmailPasswordAsync(LoginUserEmailPasswordCommand request)
        {
            var applicationUser = await GetUserByEmailAsync(request.Email);

            if (applicationUser is null)
            {
                _logger.LogWarning("Login failed: user with email {Email} not found", request.Email);
                return SignInResult.Failed;
            }

            return await _signInManager.CheckPasswordSignInAsync(applicationUser, request.Password, lockoutOnFailure: true);
        }


        public async Task<ApplicationUser?> LoginUserWithPhoneAsync(LoginUserPhoneCommand request)
        {
            return await this.GetUserByPhoneNumberAsync(request.PhoneNumber);
        }

        public async Task<ApplicationUser?> LoginUserWithEmailAsync(LoginUserEmailCommand request)
        {
            return await this.GetUserByEmailAsync(request.Email);
        }

        public async Task<ApplicationUser?> FindByFirebaseUidAsync(string firebaseUid)
        {
            return null;
            //return await _userManager.Users.FirstOrDefaultAsync(u => u.FirebaseUid == firebaseUid);
        }

        public async Task<AuthenticateResponse?> GenerateTokenAsync(string userId)
        {
            var accessToken = await tokenService.GenerateAccessToken(userId);
            if (accessToken == null)
            {
                _logger.LogInfo("Access token generation failed.");
                return null;
            }

            var ipAddress = securityService.GetIpAddress;
            var refreshToken = await jwtService.GenerateRefreshToken(userId, ipAddress);
            if (refreshToken == null)
            {
                _logger.LogInfo("Refresh token generation failed.");
                return null;
            }

            var refreshTokenEntity = refreshToken.Adapt<AspNetUserRefreshToken>();
            refreshTokenEntity.UserId = userId;
            await tokenService.SaveRefreshTokenAsync(refreshTokenEntity);

            return new AuthenticateResponse(
                accessToken.Id,
                accessToken.AccessToken,
                refreshToken.Token,
                accessToken.ExpiresIn);
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            bool isInRole = await _userManager.IsInRoleAsync(user, role);
            return isInRole;
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Authorization failed: user with ID {UserId} not found", userId);
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<List<Claim>> GetClaimsAsync(EmailDto email)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, email.Email)
        };

            var user = await _userManager.FindByEmailAsync(email.Email);
            if (user == null)
            {
                _logger.LogWarning("No user found for email {Email}", email.Email);
                return new List<Claim>();
            }

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            _logger.LogInfo("Claims generated for user {Email}", email.Email);
            return claims;
        }

        public async Task<IdentityResult?> ChangePasswordAsync(ApplicationUser user, ChangePasswordCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            return result;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, SetPasswordCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.ResetPasswordAsync(user, string.Empty, request.Password);
            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<int> AddUserDeviceAsync(string userId, CancellationToken cancellationToken = default)
        {
            var uaParser = Parser.GetDefault();
            ClientInfo client = uaParser.Parse(securityService.GetUserAgent);

            var userDevice = new AspNetUserDevice
            {
                UserId = userId,
                IpAddress = securityService.GetIpAddress,
                DeviceName = client.Device.Family,
                Browser = client.UA.Family,
                OS = client.OS.Family
            };

            unitOfWork.UserDevicesRepository.AddAsync(userDevice);
            var saveResult = await unitOfWork.SaveChangesAsync();

            return saveResult;
        }
    }
}
