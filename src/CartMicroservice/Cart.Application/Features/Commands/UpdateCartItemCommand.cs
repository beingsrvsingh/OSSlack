using MediatR;
using Shared.Utilities.Response;

namespace Cart.Application.Features.Commands
{
    public record UpdateCartItemCommand(int productVariantId, int Quantity)
    : IRequest<Result>;

}
