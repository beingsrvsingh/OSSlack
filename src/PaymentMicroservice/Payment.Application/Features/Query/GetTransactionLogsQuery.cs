using MediatR;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.Query
{
    public class GetTransactionLogsQuery : IRequest<Result>
    {
        public int TransactionId { get; set; }

        public GetTransactionLogsQuery(int transactionId)
        {
            TransactionId = transactionId;
        }
    }

}