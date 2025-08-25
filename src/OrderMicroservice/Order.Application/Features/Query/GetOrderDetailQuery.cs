using MediatR;
using Shared.Utilities.Response;

namespace Order.Application.Features.Query
{
    public record GetOrderDetailQuery(string OrderId) : IRequest<Result>;

}