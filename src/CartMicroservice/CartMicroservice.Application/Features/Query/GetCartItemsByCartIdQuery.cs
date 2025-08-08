using MediatR;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.Query
{
    public record GetCartItemsByCartIdQuery(int CartId) : IRequest<Result>;

}