using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public string UserId { get; set; } = null!;
        public string CurrentPassword { get; init; } = null!;
        public string NewPassword { get; init; } = null!;
    }
}
