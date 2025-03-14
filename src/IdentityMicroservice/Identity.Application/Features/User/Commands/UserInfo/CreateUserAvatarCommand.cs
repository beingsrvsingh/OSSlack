using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserInfo
{
    public class CreateUserAvatarCommand : IRequest<Result>
    {
        public string UserId { get; set; }
        public string Avatar { get; set; }
        public string AvatarURI { get; set; }
    }
}
