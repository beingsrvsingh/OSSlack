using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Queries
{
    public record GetTemplePrasadByIdQuery(int Id) : IRequest<Result>;

}
