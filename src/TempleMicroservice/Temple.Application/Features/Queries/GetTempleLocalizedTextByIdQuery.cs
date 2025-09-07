using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Queries
{
    public record GetTempleLocalizedTextByIdQuery(int Id) : IRequest<Result>;

}
