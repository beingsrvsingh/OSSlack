using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Queries
{
    public record GetTempleExceptionByIdQuery(int Id) : IRequest<Result>;

}
