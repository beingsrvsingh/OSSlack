using MediatR;
using System.ComponentModel.DataAnnotations;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CreateUser
{
    public class CreateUserPhoneCommand : IRequest<Result>
    {
        [Phone]
        [Display(Name = "Mobile Number")]
        public required string PhoneNumber { get; init; }

        public String RoleName { get; set; }

        public required string FirstName { get; init; }

        public string? LastName { get; init; }
    }
}
