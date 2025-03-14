using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.ChangePassword
{
    public class SetPasswordCommand : IRequest<Result>
    {
        public required string UserId { get; set; }
        public required string Token { get; set; }
        public required string Password { get; init; }
    }
}
