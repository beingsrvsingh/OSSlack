using MediatR;

namespace SecretManagement.Application.Features.Queries
{
    public record GetUserSecurityTokenQuery : IRequest<String>
    {
        public string UserId { get; set; } = null!;
    }
}
