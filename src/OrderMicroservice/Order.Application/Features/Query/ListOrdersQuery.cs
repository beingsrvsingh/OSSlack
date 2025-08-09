using MediatR;
using Shared.Utilities.Response;

namespace Order.Application.Features.Query
{
    public record ListOrdersQuery(string? userId = null) : IRequest<Result>;

}