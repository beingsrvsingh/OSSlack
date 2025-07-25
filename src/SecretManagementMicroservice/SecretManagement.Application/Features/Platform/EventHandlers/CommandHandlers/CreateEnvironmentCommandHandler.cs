using MediatR;
using SecretManagement.Application.Features.Commands;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventsHandler.CommandHandler;

public class CreateEnvironmentCommandHandler : IRequestHandler<CreateEnvironmentCommand, Result>
{
    private readonly ILoggerService<CreateEnvironmentCommandHandler> logger;
    private readonly IPlatformManagerService _platformService;

    public CreateEnvironmentCommandHandler(ILoggerService<CreateEnvironmentCommandHandler> logger, IPlatformManagerService platformService)
    {
        this.logger = logger;
        _platformService = platformService;
    }

    public async Task<Result> Handle(CreateEnvironmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool created = await _platformService.AddCredential(request.EnvironmentKey, request.EnvironmentValue);

            if (!created)
            {
                return Result.Failure(new FailureResponse(
                    "CredentialCreateFailed",
                    "The environment variable could not be saved."
                ));
            }

            return Result.Success("Credential created successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception occurred while attempting to create a new credential with key: '{KeyName}'.", request.EnvironmentKey);

            return Result.Failure(new FailureResponse(
                "CredentialError",
                "An error occurred while saving the environment credential. Please try again later."
            ));
        }
    }
}