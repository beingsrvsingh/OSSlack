using SecretManagement.Application.Services.Interfaces;
using MediatR;
using SecretManagement.Application.Features.SecretManager.Commands;
using Shared.Utilities.Response;
using SecretManagement.Domain.Entities;
using Mapster;
using Shared.Application.Interfaces.Logging;

namespace SecretManagement.Application.Features.SecretManager.EventHandlers
{
    public class CreateSecretEventCommandHandler : IRequestHandler<CreateSecretKeyCommand, Result>
    {
        private readonly ILoggerService<CreateSecretEventCommandHandler> logger;
        private ISecretsService secretService;
        public CreateSecretEventCommandHandler(ILoggerService<CreateSecretEventCommandHandler> logger, ISecretsService secretService)
        {
            this.logger = logger;
            this.secretService = secretService;
        }
        public async Task<Result> Handle(CreateSecretKeyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var secret = command.Adapt<Secret>();
                await secretService.CreateSecret(secret);
                return Result.Success("Secret created successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the secret.");
                
                return Result.Failure(new FailureResponse(
                "SecretCreationFailed",
                "An error occurred while creating the secret. Please try again later."));
            }

        }
    }
}
