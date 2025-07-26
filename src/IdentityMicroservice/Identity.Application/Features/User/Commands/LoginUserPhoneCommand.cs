using MediatR;
using System.ComponentModel.DataAnnotations;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands
{
    public class LoginUserPhoneCommand : IRequest<Result>
    {
        [Phone]
        [MaxLength(10)]
        public required int PhoneNumber { get; init; }
    }
}
