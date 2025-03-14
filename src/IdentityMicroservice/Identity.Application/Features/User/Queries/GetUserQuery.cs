using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Queries
{
    public record GetUserQuery : IRequest<Result>
    {
        public required string UserId { get; set; }
    }
}
