using MediatR;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.Query
{
    public class GetPaymentsByUserIdQuery : IRequest<Result>
    {
        public string UserId { get; }

        public GetPaymentsByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }

}