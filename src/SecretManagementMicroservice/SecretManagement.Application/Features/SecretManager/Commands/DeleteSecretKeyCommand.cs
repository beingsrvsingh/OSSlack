using Shared.Application.Common.Services.Interfaces;
using Shared.Utilities.Response;
using MediatR;

namespace SecretManagement.Application.Features.SecretManager.Commands
{
    public class DeleteSecretKeyCommand : IRequest<Result>
    {
        public string AppName { get; set; } = null!;
        public string Environment { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string SecretValue { get; set; } = null!;
    }
}
