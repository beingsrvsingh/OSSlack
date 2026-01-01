using CartMicroservice.Application.Contracts;
using MediatR;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.Commands
{
    public record UpdateCartItemCommand(UpdateCartDto CartItem) : IRequest<Result>;

}