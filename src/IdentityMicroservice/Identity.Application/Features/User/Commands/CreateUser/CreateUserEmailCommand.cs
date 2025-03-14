using MediatR;
using System.ComponentModel.DataAnnotations;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CreateUser
{
    public class CreateUserEmailCommand : IRequest<Result>
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public required string Email { get; init; }

        public required string FirstName { get; init; }

        public string? LastName { get; init; }

        public required String RoleName { get; init; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; init; }
    }
}
