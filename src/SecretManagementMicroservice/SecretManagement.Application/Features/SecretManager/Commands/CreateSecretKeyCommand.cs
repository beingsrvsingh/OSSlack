using Shared.Utilities.Response;
using MediatR;

namespace SecretManagement.Application.Features.SecretManager.Commands
{
    public class CreateSecretKeyCommand : IRequest<Result>
    {
        public required string UserId { get; set; }
        public required string AppName { get; set; } = null!;
        public required string Environment { get; set; } = null!;
        public required string SecretKey { get; set; } = null!;
        public required string SecretValue { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
        public string? Description { get; set; }

    }
}
