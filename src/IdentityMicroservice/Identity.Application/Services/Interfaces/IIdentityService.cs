using System.Security.Claims;
using Identity.Application.Contracts;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.ChangePassword;
using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public interface IIdentityService
    {
        Task<ApplicationUser?> GetUserNameAsync(string userId);
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> GetUserByPhoneNumberAsync(int phoneNumber);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<ApplicationUser?> FindByFirebaseUidAsync(string firebaseUid);

        Task<IdentityResult> CreateUserEmailAsync(CreateUserEmailCommand request, CancellationToken cancellationToken = default);
        Task<string?> CreateUserPhoneAsync(CreateUserPhoneCommand request, CancellationToken cancellationToken = default);

        Task<bool> CreateUserRoleAsync(String userId, string roleName, CancellationToken cancellationToken = default);

        Task<SignInResult?> LoginUserWithEmailPasswordAsync(LoginUserEmailPasswordCommand request);
        Task<ApplicationUser?> LoginUserWithPhoneAsync(LoginUserPhoneCommand request);
        Task<ApplicationUser?> LoginUserWithEmailAsync(LoginUserEmailCommand request);

        Task<AuthenticateResponse?> GenerateTokenAsync(string userId);

        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<List<Claim>> GetClaimsAsync(EmailDto email);

        Task<IdentityResult?> ChangePasswordAsync(ApplicationUser user, ChangePasswordCommand request, CancellationToken cancellationToken = default);

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user, CancellationToken cancellationToken = default);

        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, SetPasswordCommand request, CancellationToken cancellationToken = default);

        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);

        Task<int> AddUserDeviceAsync(string userId, CancellationToken cancellationToken = default);
    }