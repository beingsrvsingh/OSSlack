using MediatR;
using Shared.Utilities.Response;

namespace Order.Application.Features.Query
{
    public record ListOrdersQuery(int? CustomerId = null) : IRequest<Result>;

}