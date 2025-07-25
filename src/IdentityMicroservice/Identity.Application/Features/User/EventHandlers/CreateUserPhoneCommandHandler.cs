using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Domain;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class CreateUserPhoneCommandHandler : IRequestHandler<CreateUserPhoneCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly IUserService userService;
    private readonly ILoggerService<CreateUserPhoneCommandHandler> loggerService;

    public CreateUserPhoneCommandHandler(ILoggerService<CreateUserPhoneCommandHandler> loggerService, IIdentityService identityService, IUserService userService)
    {
        this.identityService = identityService;
        this.userService = userService;
        this.loggerService = loggerService;
    }

    public async Task<Result> Handle(CreateUserPhoneCommand request, CancellationToken cancellationToken)
    {
        var userId = await identityService.CreateUserPhoneAsync(request, cancellationToken);

        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure(new FailureResponse(
                "UserCreationFailed",
                "Failed to create user with the provided phone information."));
        }

        var hasRoleCreated = await identityService.CreateUserRoleAsync(userId, Roles.Customer);

        if (!hasRoleCreated)
        {
            loggerService.LogWarning("Failed to assign Customer role to user {UserId}.", userId);
        }

        await identityService.AddUserDeviceAsync(userId, cancellationToken);

        loggerService.LogInfo("User with ID {UserId} is created via phone.", userId);

        return Result.Success(userId);
    }
}