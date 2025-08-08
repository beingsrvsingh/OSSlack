using MediatR;
using Shared.Utilities.Response;

namespace Order.Application.Features.Query
{
    public record GetOrderByIdQuery(int OrderId) : IRequest<Result>;

}