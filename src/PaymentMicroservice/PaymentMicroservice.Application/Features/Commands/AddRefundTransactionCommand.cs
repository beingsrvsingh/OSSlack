using MediatR;
using PaymentMicroservice.Application.Contracts;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.Commands
{
    public class AddRefundTransactionCommand : IRequest<Result>
    {
        public RefundTransactionDto RefundDto { get; set; } = null!;
    }

}