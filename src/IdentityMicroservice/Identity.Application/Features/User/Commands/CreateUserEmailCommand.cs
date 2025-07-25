using MediatR;
using System.ComponentModel.DataAnnotations;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CreateUser
{
    public class CreateUserEmailCommand : IRequest<Result>
    {
        [EmailAddress]
        public required string Email { get; init; }
    }
}
