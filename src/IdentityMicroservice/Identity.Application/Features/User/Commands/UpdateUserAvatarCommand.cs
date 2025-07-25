using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserInfo
{
    public class UpdateUserAvatarCommand : IRequest<Result>
    {
        public required string UserId { get; set; }
        public string Avatar { get; set; } = null!;
        public string AvatarURI { get; set; } = null!; 
    }
}
