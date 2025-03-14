using MediatR;

namespace Configuration.Features.Queries
{
    public record GetUserSecurityTokenQuery : IRequest<String>
    {
        public string UserId { get; set; }
    }
}
