using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Constants;
using Shared.Utilities.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class LoginUserEmailPasswordCommandHandler : IRequestHandler<LoginUserEmailPasswordCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICookieService cookieService;
    private readonly IHttpRequestService securityService;
    private readonly ILoggerService<LoginUserEmailPasswordCommandHandler> _logger;

    public LoginUserEmailPasswordCommandHandler(ILoggerService<LoginUserEmailPasswordCommandHandler> loggerService, IIdentityService identityService, UserManager<ApplicationUser> userManager,
        ICookieService cookieService, IHttpRequestService securityService)
    {
        this.identityService = identityService;
        this._logger = loggerService;
        this.cookieService = cookieService;
        this.securityService = securityService;
        this.userManager = userManager;
    }

    public async Task<Result> Handle(LoginUserEmailPasswordCommand request, CancellationToken cancellationToken)
    {
        var signInResult = await identityService.LoginUserWithEmailPasswordAsync(request);

        if (signInResult == null)
        {
            _logger.LogWarning("Login failed: sign-in result is null for email {Email}", request.Email);
            return Result.Failure(new FailureResponse(
                code: "SignInError",
                description: "Invalid email or password."));
        }

        if (signInResult.IsLockedOut)
        {
            _logger.LogWarning("Login failed: user account locked out for email {Email}", request.Email);
            return Result.Failure(new FailureResponse(
                code: "AccountLocked",
                description: "Your account is locked. Please try again later."));
        }

        if (!signInResult.Succeeded)
        {
            _logger.LogWarning("Login failed: invalid credentials for email {Email}", request.Email);
            return Result.Failure(new FailureResponse(
                code: "InvalidCredentials",
                description: "Invalid email or password."));
        }

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            _logger.LogWarning("Login failed: user not found in database for email {Email}", request.Email);
            return Result.Failure(new FailureResponse(
                code: "UserNotFound",
                description: $"User with email {request.Email} does not exist."));
        }

        var token = await identityService.GenerateTokenAsync(user.Id);
        if (token == null)
        {
            _logger.LogError("Login failed: token generation failed for user {UserId}", user.Id);
            return Result.Failure(new FailureResponse(
                code: "TokenGenerationFailed",
                description: "Authentication token could not be generated."));
        }

        cookieService.RemoveAndSetCookie(token.RefreshToken, Constants.DEFAULT_COOKIE_PERIOD);

        await identityService.AddUserDeviceAsync(user.Id, cancellationToken);

        _logger.LogInfo("User {UserId} logged in and token created.", user.Id);

        return Result.Success(token);
    }
}