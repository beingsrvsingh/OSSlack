using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserInfo
{
    public class UpdateUserInfoCommand : IRequest<Result>
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
