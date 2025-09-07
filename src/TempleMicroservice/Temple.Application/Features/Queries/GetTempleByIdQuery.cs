using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Queries
{
    public record GetTempleByIdQuery(int Id) : IRequest<Result>;

}
