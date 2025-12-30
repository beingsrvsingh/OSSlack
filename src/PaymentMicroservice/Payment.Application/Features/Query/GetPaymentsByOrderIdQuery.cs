using MediatR;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.Query
{
    public class GetPaymentsByOrderIdQuery : IRequest<Result>
    {
        public string OrderId { get; }

        public GetPaymentsByOrderIdQuery(string orderId)
        {
            OrderId = orderId;
        }
    }

}