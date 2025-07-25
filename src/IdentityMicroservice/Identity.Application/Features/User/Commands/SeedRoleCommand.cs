using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CreateUser
{
    public record SeedRoleCommand : IRequest<Result> { }
}
