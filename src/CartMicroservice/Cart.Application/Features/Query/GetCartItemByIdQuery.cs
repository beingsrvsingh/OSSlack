using MediatR;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.Query
{
    public record GetCartItemByIdQuery(int CartItemId) : IRequest<Result>;

}