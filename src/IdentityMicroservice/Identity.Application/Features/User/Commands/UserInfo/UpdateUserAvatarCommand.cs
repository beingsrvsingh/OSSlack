using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserInfo
{
    public class UpdateUserAvatarCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string AvatarURI { get; set; }
    }
}
