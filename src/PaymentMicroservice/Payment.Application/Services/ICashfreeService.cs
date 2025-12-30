using Payment.Application.Contracts;

namespace Payment.Application.Services
{
    public interface ICashfreeService
    {
        Task<CreatePaymentResponse> CreateOrderAsync(
            string orderId,
            decimal amount,
            string customerId,
            string customerEmail,
            string customerPhone);
    }
}
