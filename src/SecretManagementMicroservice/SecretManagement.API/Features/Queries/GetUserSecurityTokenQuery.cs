using MediatR;

namespace SecretManagement.Features.Queries
{
    public record GetUserSecurityTokenQuery : IRequest<String>
    {
        public string UserId { get; set; }
    }
}
