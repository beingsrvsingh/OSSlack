using Shared.Application.Common.Services.Interfaces;
using SecretManagement.Application.Services.Interfaces;
using MediatR;
using SecretManagement.Application.Features.SecretManager.Commands;
using Shared.Utilities.Response;
using SecretManagement.Domain.Entities;
using Mapster;

namespace SecretManagement.Application.Features.SecretManager.EventHandlers
{
    public class CreateSecretEventCommandHandler : IRequestHandler<CreateSecretKeyCommand, Result>
    {
        private readonly ILoggerService logger;
        private ISecretsService secretService;
        public CreateSecretEventCommandHandler(ILoggerService logger, ISecretsService secretService)
        {
            this.logger = logger;
            this.secretService = secretService;
        }
        public async Task<Result> Handle(CreateSecretKeyCommand command, CancellationToken cancellationToken)
        {
            var secret = command.Adapt<Secret>();
            await secretService.CreateSecret(secret);
            return Result.Success("Secret created successfully.");
        }
    }
}
