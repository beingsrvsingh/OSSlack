using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities;
using Shared.Utilities.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class LoginUserEmailCommandHandler : IRequestHandler<LoginUserEmailCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly ICookieService cookieService;
    private readonly IHttpRequestService securityService;
    private readonly IFirebaseAuthService _firebaseAuthService;
    private readonly ILoggerService<LoginUserEmailCommandHandler> _logger;

    public LoginUserEmailCommandHandler(ILoggerService<LoginUserEmailCommandHandler> loggerService, IIdentityService identityService,
        ICookieService cookieService, IHttpRequestService securityService, IFirebaseAuthService firebaseAuthService)
    {
        this.identityService = identityService;
        this._logger = loggerService;
        this.cookieService = cookieService;
        this.securityService = securityService;
        this._firebaseAuthService = firebaseAuthService;
    }

    public async Task<Result> Handle(LoginUserEmailCommand request, CancellationToken cancellationToken)
    {
        //Uncoment the code once the firebase in-house

        // var payload = await _firebaseAuthService.VerifyTokenAndGetPayloadAsync(request.FirebaseIdToken);
        // if (payload == null)
        // {
        //     _logger.LogWarning("Firebase token validation failed.");
        //     return Result.Failure(new FailureResponse("InvalidFirebaseToken", "Firebase token validation failed."));
        // }

        // var firebaseUserId = payload.Subject;
        // payload.Claims.TryGetValue("email", out var emailObj);
        // var emailFromToken = emailObj?.ToString();

        // if (string.IsNullOrWhiteSpace(emailFromToken) || !string.Equals(emailFromToken, request.Email.ToString(), StringComparison.OrdinalIgnoreCase))
        // {
        //     _logger.LogWarning("Email from Firebase token does not match the requested email.");
        //     return Result.Failure(new FailureResponse("EmailIdMismatch", "Email Id in token does not match the requested email."));
        // }

        // var user = await identityService.FindByFirebaseUidAsync(firebaseUserId);

        var user = await identityService.LoginUserWithEmailAsync(request);

        if (user == null)
        {
            _logger.LogWarning("Login failed: No user found for email {Email}.", request.Email);
            return Result.Failure(new FailureResponse(
                code: "UserNotFound",
                description: $"No user associated with email {request.Email}."));
        }

        // if (!string.Equals(user.Email, emailFromToken, StringComparison.OrdinalIgnoreCase))
        // {
        //     _logger.LogWarning("Email mismatch for user with UserId {UserId}.", user!.Id);
        //     return Result.Failure(new FailureResponse("EmailIdMismatch", "Email mismatch with user record."));
        // }

        if (user.LockoutEnabled)
        {
            _logger.LogWarning("Login failed: User account locked out for user {UserId}.", user.Id);
            return Result.Failure(new FailureResponse(
                code: "AccountLocked",
                description: "Your account is locked. Please try again later."));
        }

        if (!user.IsActive || user.IsDeleted)
        {
            _logger.LogWarning("Login failed: User account deactivated or deleted for user {UserId}.", user.Id);
            return Result.Failure(new FailureResponse(
                code: "AccountInactive",
                description: "User account is not active."));
        }

        _logger.LogInfo("User {UserId} logged in successfully.", user.Id);

        await identityService.AddUserDeviceAsync(user.Id);

        var token = await identityService.GenerateTokenAsync(user.Id);

        if (token == null)
        {
            _logger.LogError("Failed to generate authentication token for user {UserId}.", user.Id);
            return Result.Failure(new FailureResponse(
                code: "TokenGenerationFailed",
                description: "Failed to generate authentication token."));
        }

        _logger.LogInfo("Token created successfully for user {UserId}.", user.Id);

        // Set Refresh token in cookie to validate from cookie.
        cookieService.RemoveAndSetCookie(token.RefreshToken, Constants.DEFAULT_COOKIE_PERIOD);

        return Result.Success(token);
    }
}