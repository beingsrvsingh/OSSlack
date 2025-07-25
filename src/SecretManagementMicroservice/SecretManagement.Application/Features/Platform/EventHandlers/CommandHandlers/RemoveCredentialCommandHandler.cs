using MediatR;
using SecretManagement.Application.Features.Commands;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventHandlers.CommandHandlers;

public class RemoveCredentialCommandHandler : IRequestHandler<RemoveCredentialCommand, Result>
{
    private readonly ILoggerService<RemoveCredentialCommandHandler> logger;
    private readonly IPlatformManagerService _platformService;

    public RemoveCredentialCommandHandler(ILoggerService<RemoveCredentialCommandHandler> logger, IPlatformManagerService platformService)
    {
        this.logger = logger;
        _platformService = platformService;
    }

    public async Task<Result> Handle(RemoveCredentialCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool removed = await _platformService.RemoveCredential(request.KeyName);

            if (!removed)
            {
                return Result.Failure(new FailureResponse(
                    "CredentialNotFound",
                    "The credential could not be found or removed."
                ));
            }

            return Result.Success("Credential removed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception occurred while attempting to remove the credential with key: '{KeyName}'.", request.KeyName);

            return Result.Failure(new FailureResponse(
                "CredentialRemovalFailed",
                "An error occurred while removing the credential. Please try again later."
            ));
        }
    }
}
