using MediatR;
using Shared.Utilities.Response;

namespace Order.Application.Features.Commands
{
    public class UpdateOrderStatusCommand : IRequest<Result>
    {
        public required string OrderNumber { get; set; }
        public required string Status { get; set; }
    }
}
