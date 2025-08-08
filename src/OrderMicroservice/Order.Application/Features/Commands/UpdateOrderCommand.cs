using MediatR;
using Order.Application.Contracts;
using Shared.Utilities.Response;

namespace Order.Application.Features.Commands
{
    public record UpdateOrderCommand(OrderDto Order) : IRequest<Result>;

}