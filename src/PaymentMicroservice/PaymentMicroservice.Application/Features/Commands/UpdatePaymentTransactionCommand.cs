using MediatR;
using PaymentMicroservice.Application.Contracts;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.Commands
{
    public class UpdatePaymentTransactionCommand : IRequest<Result>
{
    public PaymentTransactionDto TransactionDto { get; set; } = null!;
}

}