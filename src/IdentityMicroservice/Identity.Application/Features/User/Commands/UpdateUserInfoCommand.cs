using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserInfo
{
    public class UpdateUserInfoCommand : IRequest<Result>
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
