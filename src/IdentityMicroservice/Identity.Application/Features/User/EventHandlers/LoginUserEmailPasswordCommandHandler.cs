using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities;
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
            _logger.LogWarning("Sign-in result is null.");
            return Result.Failure("UserId or Password is incorrect");
        }

        if (signInResult.IsLockedOut)
        {
            _logger.LogWarning("User account locked out.");
            return Result.Failure("UserId or Password is incorrect");
        }

        if (!signInResult.Succeeded)
        {
            _logger.LogInfo("User login failed.");
            return Result.Failure("UserId or Password is incorrect");
        }

        _logger.LogInfo("User logged in.");

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            _logger.LogInfo("User does not exist.");
            return Result.Failure("UserId or Password is incorrect");
        }

        _logger.LogInfo("User exists.");

        var token = await identityService.GenerateTokenAsync(user.Id);

        if (token == null)
        {
            _logger.LogInfo("Token creation failed.");
            return Result.Failure("UserId or Password is incorrect");
        }

        _logger.LogInfo("Token created.");

        // Set Refresh token in cookie to validate from cookie.
        cookieService.RemoveAndSetCookie(token.RefreshToken, Constants.DEFAULT_COOKIE_PERIOD);

        await identityService.AddUserDeviceAsync(user.Id, cancellationToken);

        return Result.Success(token);
    }
}