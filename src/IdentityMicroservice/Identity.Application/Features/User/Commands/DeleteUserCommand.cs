using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public string UserId { get; set; } = null!;
    }
}
