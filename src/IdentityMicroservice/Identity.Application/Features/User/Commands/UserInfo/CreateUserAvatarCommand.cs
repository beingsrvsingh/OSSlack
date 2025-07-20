using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserInfo
{
    public class CreateUserAvatarCommand : IRequest<Result>
    {
        public string UserId { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string AvatarURI { get; set; } = null!;
    }
}
