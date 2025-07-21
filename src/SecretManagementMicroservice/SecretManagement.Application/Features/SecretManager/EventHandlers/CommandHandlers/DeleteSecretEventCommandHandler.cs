using SecretManagement.Application.Services.Interfaces;
using MediatR;
using SecretManagement.Application.Features.SecretManager.Commands;
using Shared.Utilities.Response;
using Shared.Application.Interfaces.Logging;

namespace SecretManagement.Application.Features.SecretManager.EventHandlers
{
    public class DeleteSecretEventCommandHandler : IRequestHandler<DeleteSecretKeyCommand, Result>
    {
        private readonly ILoggerService logger;
        private readonly ISecretsService secretService;
        public DeleteSecretEventCommandHandler(ILoggerService logger, ISecretsService secretService)
        {
            this.logger = logger;
            this.secretService = secretService;
        }
        public Task<Result> Handle(DeleteSecretKeyCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success());
        }
    }
}
