using SecretManagement.Application.Services.Interfaces;
using MediatR;
using SecretManagement.Application.Features.SecretManager.Commands;
using Shared.Utilities.Response;
using Shared.Application.Interfaces.Logging;

namespace SecretManagement.Application.Features.SecretManager.EventHandlers
{
    public class DeleteSecretEventCommandHandler : IRequestHandler<DeleteSecretKeyCommand, Result>
    {
        private readonly ILoggerService<DeleteSecretEventCommandHandler> logger;
        private readonly ISecretsService secretService;
        public DeleteSecretEventCommandHandler(ILoggerService<DeleteSecretEventCommandHandler> logger, ISecretsService secretService)
        {
            this.logger = logger;
            this.secretService = secretService;
        }
        public async Task<Result> Handle(DeleteSecretKeyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await secretService.DeleteSecret(command.SecretKey, command.AppName, command.Environment);
                return Result.Success("Secret deleted successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting the secret with key {SecretKey}.", command.SecretKey);

                return Result.Failure(new FailureResponse(
                    "SecretDeleteFailed",
                    "An error occurred while deleting the secret. Please try again later."
                ));
            }
        }
    }
}
