using MediatR;
using System.ComponentModel.DataAnnotations;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CreateUser
{
    public class CreateUserPhoneCommand : IRequest<Result>
    {
        [Phone]
        public required string PhoneNumber { get; init; }
    }
}
