using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; init; }
        public string NewPassword { get; init; }
    }
}
