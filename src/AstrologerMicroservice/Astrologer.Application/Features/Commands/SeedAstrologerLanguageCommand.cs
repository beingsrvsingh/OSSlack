using MediatR;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.Admin.Commands
{
    public record SeedAstrologerLanguageCommand : IRequest<Result>{}
}