using MediatR;
using Order.Application.Contracts;
using Shared.Utilities.Response;

namespace Order.Application.Features.Commands
{
    public record AddOrderCommand() : IRequest<Result>
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }

        public OrderShippingAddressDto? ShippingAddress { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }

}