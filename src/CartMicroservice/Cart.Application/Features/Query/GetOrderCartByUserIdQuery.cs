using MediatR;
using Shared.Utilities.Response;

namespace Cart.Application.Features.Query
{
    public record GetOrderCartByUserIdQuery(string UserId) : IRequest<Result>;
}
