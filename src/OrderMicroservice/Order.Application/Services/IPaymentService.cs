using Shared.Application.Contracts;

namespace Order.Application.Services
{
    public interface IPaymentService
    {
        Task<PaymentInfoDto?> GetPaymentInfoByIdAsync(int orderId);
    }
}