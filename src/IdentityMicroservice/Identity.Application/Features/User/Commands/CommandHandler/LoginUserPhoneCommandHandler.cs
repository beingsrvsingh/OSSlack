using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Utilities;
using Utilities.Services;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class LoginUserPhoneCommandHandler : IRequestHandler<LoginUserPhoneCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly ICookieService cookieService;
    private readonly ISecurityService securityService;
    private readonly ILoggerService _logger;

    public LoginUserPhoneCommandHandler(IIdentityService identityService, ILoggerService loggerService, 
        ICookieService cookieService, ISecurityService securityService)
    {
        this.identityService = identityService;
        this._logger = loggerService;
        this.cookieService = cookieService;
        this.securityService = securityService;
    }

    public async Task<Result> Handle(LoginUserPhoneCommand request, CancellationToken cancellationToken)
    {
        var signInResult = await this.identityService.LoginAsync(request);

        if (signInResult!.LockoutEnabled)
        {
            _logger.LogWarning("User account locked out.");
            return Result.Failure("UserId or Password is incorrect");
        }

        if (signInResult!.IsActive || signInResult.IsDeleted)
        {
            _logger.LogWarning("User account either is deactivate or deleted.");
            return Result.Failure("Phone not found.");
        }

        if (signInResult is not null)
        {
            _logger.LogInfo("User logged in.");

            await this.identityService.AddUserDevicesAsync(signInResult.Id);

            var token = await this.identityService.GenerateTokenAsync(signInResult.Id);

            _logger.LogInfo("token is null.");

            if (token != null)
            {
                //Set Refresh token in cookie to validate from cookie.
                cookieService.RemoveAndSetCookie(token.RefreshToken, Constants.DEFAULT_COOKIE_PERIOD);

                return Result.Success(token);
            }
        }

        return Result.Failure("Phone Number does not exist.");
    }
}