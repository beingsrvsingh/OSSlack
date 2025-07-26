

using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class DeActivateUserCommandHandler : IRequestHandler<DeActivateUserCommand, Result>
{
    private readonly ILoggerService<DeActivateUserCommandHandler> _logger;
    private readonly IIdentityService _identityService;
    private readonly IUserService userService;

    public DeActivateUserCommandHandler(ILoggerService<DeActivateUserCommandHandler> logger, IIdentityService identityService, IUserService userService)
    {
        this._logger = logger;
        _identityService = identityService;
        this.userService = userService;
    }

    public async Task<Result> Handle(DeActivateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInfo("Processing user activation request for UserId: {UserId}, IsActive: {IsActive}", request.UserId, request.IsActive);

        var hasActivated = await userService.DeActivateUserAsync(request, cancellationToken);

        if (!hasActivated)
        {
            _logger.LogWarning("Failed to update activation status for UserId: {UserId}", request.UserId);
            return Result.Failure(new FailureResponse("ActivationFailed", "User activation/deactivation failed."));
        }

        var action = "activated";
        _logger.LogInfo("User {UserId} has been successfully {Action}.", request.UserId, action);

        return Result.Success($"User has been {action} successfully.");
    }

}
