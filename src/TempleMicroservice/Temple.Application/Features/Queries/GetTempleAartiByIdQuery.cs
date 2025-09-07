using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Queries
{
    public record GetTempleAartiByIdQuery(int Id) : IRequest<Result>;

}
