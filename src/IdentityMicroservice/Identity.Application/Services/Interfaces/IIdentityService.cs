using System.Security.Claims;
using Identity.Application.Contracts;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.ChangePassword;
using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<ApplicationUser?> GetUserNameAsync(string userId);

        Task<ApplicationUser?> GetUserByIdAsync(string id);

        Task<ApplicationUser?> GetUserByPhoneNumberAsync(int phoneNumber);

        Task<ApplicationUser?> GetUserByEmailAsync(string email);

        Task<ApplicationUser?> GetUserAsync(string userId);

        Task<IdentityResult> CreateUserAsync(CreateUserEmailCommand request, CancellationToken cancellationToken = default);

        Task CreateUserRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default);

        Task<String?> CreateUserPhoneAsync(CreateUserPhoneCommand request, CancellationToken cancellationToken = default);

        Task CreateSigningKeyAsync(string userId, CancellationToken cancellationToken = default);

        Task<SignInResult?> LoginAsync(LoginUserEmailCommand request);

        Task<ApplicationUser?> LoginAsync(LoginUserPhoneCommand request);

        Task<AuthenticateResponse?> GenerateTokenAsync(string userId);

        Task<List<Claim>> GetClaims(EmailDto email);

        Task<bool> IsInRoleAsync(ApplicationUser user, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);

        Task ChangePasswordAsync(ApplicationUser user, ChangePasswordCommand request, CancellationToken cancellationToken = default);

        Task<string> ForgotPasswordAsync(ApplicationUser user, CancellationToken cancellationToken = default);

        Task SetPasswordAsync(ApplicationUser user, SetPasswordCommand request, CancellationToken cancellationToken = default);

        Task<int> AddUserDevicesAsync(String userId, CancellationToken cancellationToken = default);
    }
}
