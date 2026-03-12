using MediatR;
using Shared.Utilities.Response;

namespace Order.Application.Features.Commands
{
    public record AddOrderCommand() : IRequest<Result>
    {
        public required string BookingRefNum { get; set; }
    }

}