using SecretManagement.Application.Services.Interfaces;
using MediatR;
using SecretManagement.Application.Features.SecretManager.Commands;
using Shared.Utilities.Response;
using SecretManagement.Domain.Entities;
using Mapster;
using Shared.Application.Interfaces.Logging;


namespace SecretManagement.Application.Features.SecretManager.EventHandlers
{
    public class UpdateSecretEventCommandHandler : IRequestHandler<UpdateSecretKeyCommand, Result>
    {
        private readonly ILoggerService<UpdateSecretEventCommandHandler> logger;
        private ISecretsService secretService;
        public UpdateSecretEventCommandHandler(ILoggerService<UpdateSecretEventCommandHandler> logger, ISecretsService secretService)
        {
            this.logger = logger;
            this.secretService = secretService;
        }
        public async Task<Result> Handle(UpdateSecretKeyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var secret = command.Adapt<Secret>();
                await secretService.UpdateSecret(secret);
                return Result.Success("Secret updated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating a secret.");
                return Result.Failure(new FailureResponse(
                "SecretUpdateFailed",
                "An unexpected error occurred while updating the secret. Please try again later."));
            }
        }
    }
}
