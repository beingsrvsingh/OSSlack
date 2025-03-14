using MediatR;

namespace Shared.JwtTokenAuthentication.Application.Features.Queries
{
    public record GetUserRoleQuery : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string Role { get; set; }
    }
}
