using MediatR;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.Commands
{
    public class DeletePaymentTransactionCommand : IRequest<Result>
    {
        public int TransactionId { get; }

        public DeletePaymentTransactionCommand(int transactionId)
        {
            TransactionId = transactionId;
        }
    }

}