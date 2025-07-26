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
    private readonly ILoggerService<LoginUserEmailCommandHandler> _logger;

    public LoginUserEmailCommandHandler(ILoggerService<LoginUserEmailCommandHandler> loggerService, IIdentityService identityService,
        ICookieService cookieService, IHttpRequestService securityService)
    {
        this.identityService = identityService;
        this._logger = loggerService;
        this.cookieService = cookieService;
        this.securityService = securityService;
    }

    public async Task<Result> Handle(LoginUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.LoginUserWithEmailAsync(request);

        if (user == null)
        {
            _logger.LogWarning("Email Id does not exist.");
            return Result.Failure("Email Id does not exist.");
        }

        if (user.LockoutEnabled)
        {
            _logger.LogWarning("User account locked out.");
            return Result.Failure("UserId or Password is incorrect.");
        }

        if (!user.IsActive || user.IsDeleted)
        {
            _logger.LogWarning("User account either is deactivated or deleted.");
            return Result.Failure("Email not found.");
        }

        _logger.LogInfo("User logged in.");

        await identityService.AddUserDeviceAsync(user.Id);

        var token = await identityService.GenerateTokenAsync(user.Id);

        if (token == null)
        {
            _logger.LogWarning("Failed to create token for user {UserId}.", user.Id);
            return Result.Failure("Failed to generate authentication token.");
        }

        _logger.LogInfo("Token created successfully.");

        // Set Refresh token in cookie to validate from cookie.
        cookieService.RemoveAndSetCookie(token.RefreshToken, Constants.DEFAULT_COOKIE_PERIOD);

        return Result.Success(token);
    }
}