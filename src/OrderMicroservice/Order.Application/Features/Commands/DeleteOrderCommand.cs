using MediatR;
using Order.Application.Contracts;
using Shared.Utilities.Response;

namespace Order.Application.Features.Commands
{
    public record DeleteOrderCommand(int OrderId) : IRequest<Result>;

}