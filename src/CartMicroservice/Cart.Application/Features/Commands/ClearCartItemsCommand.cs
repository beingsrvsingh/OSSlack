using MediatR;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.Commands
{
    public record ClearCartItemsCommand(int CartId) : IRequest<Result>;

}