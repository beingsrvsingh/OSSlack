using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities;
using Shared.Utilities.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class LoginUserPhoneCommandHandler : IRequestHandler<LoginUserPhoneCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly ICookieService cookieService;
    private readonly IHttpRequestService securityService;
    private readonly IFirebaseAuthService _firebaseAuthService;
    private readonly ILoggerService<LoginUserPhoneCommandHandler> _logger;

    public LoginUserPhoneCommandHandler(ILoggerService<LoginUserPhoneCommandHandler> loggerService, IIdentityService identityService,
        ICookieService cookieService, IHttpRequestService securityService, IFirebaseAuthService firebaseAuthService)
    {
        this.identityService = identityService;
        this._logger = loggerService;
        this.cookieService = cookieService;
        this.securityService = securityService;
        this._firebaseAuthService = firebaseAuthService;
    }

    public async Task<Result> Handle(LoginUserPhoneCommand request, CancellationToken cancellationToken)
    {
        var payload = await _firebaseAuthService.VerifyTokenAndGetPayloadAsync(request.FirebaseIdToken);
        if (payload == null)
        {
            _logger.LogWarning("Firebase token validation failed.");
            return Result.Failure(new FailureResponse("InvalidFirebaseToken", "Firebase token validation failed."));
        }

        var firebaseUserId = payload.Subject;
        var phoneNumberFromToken = payload.Claims.ContainsKey("phone_number") ? payload.Claims["phone_number"].ToString() : null;

        if (phoneNumberFromToken == null || phoneNumberFromToken != request.PhoneNumber.ToString())
        {
            _logger.LogWarning("Phone number from Firebase token does not match request.");
            return Result.Failure(new FailureResponse("PhoneNumberMismatch", "Phone number in token does not match the requested phone."));
        }        

        var user = await identityService.FindByFirebaseUidAsync(firebaseUserId);

        if (user == null)
        {
            _logger.LogWarning("Login failed: User with Firebase UID {FirebaseUid} and phone number {Phone} not found.", firebaseUserId, request.PhoneNumber);
            return Result.Failure(new FailureResponse(
                code: "UserNotFound",
                description: $"No user associated with phone number {request.PhoneNumber}."));
        }

        if (!string.Equals(user.PhoneNumber, phoneNumberFromToken, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning($"Phone Number mismatch for user with Firebase UID {firebaseUserId}.");
            return Result.Failure(new FailureResponse("PhoneNumberMismatch", "Phone Number mismatch with user record."));
        }

        if (user.LockoutEnabled)
        {
            _logger.LogWarning("Login failed: user account is locked out. Phone: {Phone}", request.PhoneNumber);
            return Result.Failure(new FailureResponse(
                code: "UserLocked",
                description: "User account is locked."));
        }

        if (!user.IsActive || user.IsDeleted)
        {
            _logger.LogWarning("Login failed: user {UserId} is deactivated or deleted.", user.Id);
            return Result.Failure(new FailureResponse(
                code: "UserInactiveOrDeleted",
                description: "The user account is either deactivated or deleted."));
        }

        _logger.LogInfo("User {UserId} authenticated successfully.", user.Id);

        await identityService.AddUserDeviceAsync(user.Id, cancellationToken);

        var token = await identityService.GenerateTokenAsync(user.Id);

        if (token == null)
        {
            _logger.LogError("Token generation failed for user {UserId}.", user.Id);
            return Result.Failure(new FailureResponse(
                code: "TokenGenerationFailed",
                description: "Failed to generate authentication token."));
        }

        _logger.LogInfo("Token generated successfully for user {UserId}.", user.Id);

        cookieService.RemoveAndSetCookie(token.RefreshToken, Constants.DEFAULT_COOKIE_PERIOD);

        return Result.Success(token);
    }
}