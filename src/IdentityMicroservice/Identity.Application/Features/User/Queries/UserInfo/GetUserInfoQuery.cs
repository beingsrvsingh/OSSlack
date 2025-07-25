using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Queries.UserInfo
{
    public record GetUserInfoQuery : IRequest<Result>
    {
        public string UserId { get; set; } = null!;
    };
}
