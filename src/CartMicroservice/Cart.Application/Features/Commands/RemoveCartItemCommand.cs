using MediatR;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.Commands
{
    public record RemoveCartItemCommand(int CartItemId) : IRequest<Result>;

}