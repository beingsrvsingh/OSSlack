using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Admin.Commands
{
    public record SeedAstrologerLanguageCommand : IRequest<Result>{}
}