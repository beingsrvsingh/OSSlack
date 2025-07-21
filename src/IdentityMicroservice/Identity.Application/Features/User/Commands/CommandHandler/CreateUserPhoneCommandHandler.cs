using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Application.Features.User.Commands.UserInfo;
using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class CreateUserPhoneCommandHandler : IRequestHandler<CreateUserPhoneCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly IUserService userService;
    private readonly ILoggerService loggerService;

    public CreateUserPhoneCommandHandler(IIdentityService identityService, IUserService userService, ILoggerService loggerService)
    {
        this.identityService = identityService;
        this.userService = userService;
        this.loggerService = loggerService;
    }

    public async Task<Result> Handle(CreateUserPhoneCommand request, CancellationToken cancellationToken)
    {
        String? userId = await identityService.CreateUserPhoneAsync(request, cancellationToken);                

        if(!string.IsNullOrEmpty(userId))
        {
            await this.identityService.CreateSigningKeyAsync(userId);

            await this.identityService.CreateUserRoleAsync(userId, request.RoleName);

            loggerService.LogInfo("{0} is created", request.PhoneNumber);
            CreateUserInfoCommand userInfoRequest = new() { FirstName = request.FirstName.Trim(), UserId = userId, LastName = request?.LastName?.Trim() ?? null };

            await userService.CreateUserInfoAsync(userInfoRequest, cancellationToken);
            await this.identityService.AddUserDevicesAsync(userId, cancellationToken);

            loggerService.LogInfo($"{request?.FirstName} is created");

            return Result.Success(userId);
        }

        return Result.Failure();

    }
}