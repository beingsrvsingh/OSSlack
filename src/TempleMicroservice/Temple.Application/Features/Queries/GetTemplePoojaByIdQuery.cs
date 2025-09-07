using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Queries
{
    public record GetTemplePoojaByIdQuery(int Id) : IRequest<Result>;

}
