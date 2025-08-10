using MediatR;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.Commands
{
    public record DeleteAstrologerCommand(int Id) : IRequest<Result>;
}