using Payment.Application.Contracts;

namespace Payment.Application.Services
{
    public interface ICashfreeService
    {
        Task<CreatePaymentResponse> CreateOrderAsync(string orderId,string userId,decimal amount,string currency);
    }
}
