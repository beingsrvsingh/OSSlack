using MediatR;
using PaymentMicroservice.Application.Contracts;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.Commands
{
    public class AddPaymentTransactionCommand : IRequest<Result>
    {
        public required string OrderNumber { get; set; }
        public string UserId { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
    }

}