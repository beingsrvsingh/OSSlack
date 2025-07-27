using MediatR;
using SecretManagement.Application.Features.Platform.Commands;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventsHandler.CommandHandler;

public class CreateEnvironmentsCommandHandler : IRequestHandler<CreateEnvironmentsCommand, Result>
{
    private readonly ILoggerService<CreateEnvironmentsCommandHandler> logger;
    private readonly IPlatformManagerService _platformService;

    public CreateEnvironmentsCommandHandler(ILoggerService<CreateEnvironmentsCommandHandler> logger, IPlatformManagerService platformService)
    {
        this.logger = logger;
        _platformService = platformService;
    }

    public async Task<Result> Handle(CreateEnvironmentsCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<string>();

        foreach (var credential in request.Credentials)
        {
            try
            {
                bool created = await _platformService.AddCredential(credential.EnvironmentKey, credential.EnvironmentValue);

                if (!created)
                {
                    errors.Add($"Failed to create credential with key '{credential.EnvironmentKey}'.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception while creating credential with key: '{KeyName}'.", credential.EnvironmentKey);
                errors.Add($"Error creating credential with key '{credential.EnvironmentKey}'.");
            }
        }

        if (errors.Any())
        {
            return Result.Failure(new FailureResponse(
                "BatchCredentialCreateFailed",
                $"Some credentials failed to be created: {string.Join("; ", errors)}"
            ));
        }

        return Result.Success("All credentials created successfully.");
    }
}