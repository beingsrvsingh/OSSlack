﻿using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Application.Common.Services.Interfaces;
using Shared.Utilities.Response;
using UAParser;
using Utilities;
using Utilities.Services;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class LoginUserEmailCommandHandler : IRequestHandler<LoginUserEmailCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICookieService cookieService;
    private readonly ISecurityService securityService;
    private readonly ILoggerService _logger;

    public LoginUserEmailCommandHandler(IIdentityService identityService, UserManager<ApplicationUser> userManager, ILoggerService loggerService, 
        ICookieService cookieService, ISecurityService securityService)
    {
        this.identityService = identityService;
        this._logger = loggerService;
        this.cookieService = cookieService;
        this.securityService = securityService;
        this.userManager = userManager;
    }

    public async Task<Result> Handle(LoginUserEmailCommand request, CancellationToken cancellationToken)
    {
        var signInResult = await this.identityService.LoginAsync(request);

        if (signInResult!.IsLockedOut)
        {
            _logger.LogWarning("User account locked out.");
            return Result.Failure("UserId or Password is incorrect");
        }

        if (signInResult.Succeeded)
        {
            _logger.LogInfo("User logged in.");

            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is not null)
            {
                _logger.LogInfo("User exists.");
                var token = await this.identityService.GenerateTokenAsync(user.Id);

                if (token != null)
                {
                    _logger.LogInfo("token created.");
                    //Set Refresh token in cookie to validate from cookie.
                    cookieService.RemoveAndSetCookie(token.RefreshToken, Constants.DEFAULT_COOKIE_PERIOD);

                    await this.identityService.AddUserDevicesAsync(user.Id, cancellationToken);

                    return Result.Success(token);
                }
            }            
        }

        _logger.LogInfo("Either user or token does not exist.");

        return Result.Failure("UserId or Password is incorrect");
    }
}