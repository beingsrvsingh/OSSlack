using MediatR;
using Order.Application.Contracts;
using Shared.Utilities.Response;

namespace Order.Application.Features.Commands
{
    public record AddOrderCommand() : IRequest<Result>
    {
        public int BookingId { get; set; }
    }

}