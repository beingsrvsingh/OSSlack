using MediatR;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.Query
{
    public class GetPaymentsByOrderIdQuery : IRequest<Result>
    {
        public int OrderId { get; }

        public GetPaymentsByOrderIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }

}