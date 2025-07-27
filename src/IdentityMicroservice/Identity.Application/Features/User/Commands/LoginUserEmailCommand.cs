using MediatR;
using System.ComponentModel.DataAnnotations;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands
{
    public class LoginUserEmailCommand : IRequest<Result>
    {
        [EmailAddress]
        public required string Email { get; init; }
        public required string FirebaseIdToken { get; set; }
    }
}
