using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Application.Features.User.Commands.UserInfo;
using Identity.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Shared.Utilities.Response.Extensions;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class CreateUserEmailCommandHandler : IRequestHandler<CreateUserEmailCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly ILoggerService<CreateUserEmailCommandHandler> loggerService;
    private readonly IUserService userService;

    public CreateUserEmailCommandHandler(ILoggerService<CreateUserEmailCommandHandler> loggerService, IIdentityService identityService, IUserService userService)
    {
        this.identityService = identityService;
        this.loggerService = loggerService;
        this.userService = userService;
    }

    public async Task<Result> Handle(CreateUserEmailCommand request, CancellationToken cancellationToken)
    {
        IdentityResult result = await identityService.CreateUserAsync(request, cancellationToken);

        loggerService.LogInfo("{0} is created", request.Email);

        if(result.Succeeded)
        {
            var user = await this.identityService.GetUserByEmailAsync(request.Email);
            if (user != null)
            {
                CreateUserInfoCommand userInfoRequest = new() { FirstName = request.FirstName.Trim(), UserId = user.Id, LastName = request?.LastName?.Trim() ?? null };
                await userService.CreateUserInfoAsync(userInfoRequest, cancellationToken);
                await this.identityService.CreateUserRoleAsync(user.Id, request.RoleName);
                await this.identityService.AddUserDevicesAsync(user.Id, cancellationToken);

                loggerService.LogInfo("Signing key is generated for {0}", request!.Email);

                return result.ToApplicationResult(user.Id);
            }            
        }

        return Result.Failure(result.Errors);

    }
}