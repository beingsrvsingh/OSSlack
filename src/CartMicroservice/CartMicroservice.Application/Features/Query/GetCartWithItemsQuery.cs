using MediatR;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.Query
{
    public record GetCartWithItemsQuery(int CartId) : IRequest<Result>;

}