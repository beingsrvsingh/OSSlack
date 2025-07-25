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
    private readonly ILoggerService<LoginUserPhoneCommandHandler> _logger;

    public LoginUserPhoneCommandHandler(ILoggerService<LoginUserPhoneCommandHandler> loggerService, IIdentityService identityService,
        ICookieService cookieService, IHttpRequestService securityService)
    {
        this.identityService = identityService;
        this._logger = loggerService;
        this.cookieService = cookieService;
        this.securityService = securityService;
    }

    public async Task<Result> Handle(LoginUserPhoneCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.LoginUserWithPhoneAsync(request);

        if (user == null)
        {
            _logger.LogWarning("Phone Number does not exist.");
            return Result.Failure("Phone Number does not exist.");
        }

        if (user.LockoutEnabled)
        {
            _logger.LogWarning("User account locked out.");
            return Result.Failure("UserId or Password is incorrect.");
        }

        if (!user.IsActive || user.IsDeleted)
        {
            _logger.LogWarning("User account either is deactivated or deleted.");
            return Result.Failure("Phone not found.");
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