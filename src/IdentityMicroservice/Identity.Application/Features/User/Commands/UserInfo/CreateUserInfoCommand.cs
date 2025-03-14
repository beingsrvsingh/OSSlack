using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserInfo
{
    public class CreateUserInfoCommand : IRequest<Result>
    {
        public required string UserId { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }    
}
