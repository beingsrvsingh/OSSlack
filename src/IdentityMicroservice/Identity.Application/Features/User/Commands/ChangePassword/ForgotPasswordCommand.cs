using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.ChangePassword
{
    public class ForgotPasswordCommand : IRequest<Result>
    {
        public string Email { get; init; }
    }
}
