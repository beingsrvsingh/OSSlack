using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.Token
{
    public class RefreshTokenCommand : IRequest<Result>
    {
        public required string UserId { get; set; }
        public required String RefreshToken { get; set; }
    }
}
