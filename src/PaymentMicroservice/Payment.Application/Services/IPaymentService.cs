using PaymentMicroservice.Domain.Entities;
using Shared.Application.Contracts;

namespace PaymentMicroservice.Application.Services
{
    public interface IPaymentService
    {
        Task<PaymentTransaction?> GetPaymentTransactionByIdAsync(int id);
        Task<PaymentInfoDto?> GetPaymentTransactionByOrderIdAsync(int orderId);
        Task<IEnumerable<PaymentTransaction>> GetPaymentsByUserIdAsync(string userId);
        Task<IEnumerable<PaymentTransactionLog>> GetTransactionLogsAsync(int paymentTransactionId);
        Task<IEnumerable<RefundTransaction>> GetRefundsByTransactionIdAsync(int paymentTransactionId);
        Task<PaymentMethodDetails?> GetPaymentMethodByIdAsync(int id);

        Task<bool> AddPaymentTransactionAsync(PaymentTransaction transaction);
        Task<bool> AddTransactionLogAsync(PaymentTransactionLog log);
        Task<bool> AddRefundTransactionAsync(RefundTransaction refund);
        Task<bool> AddPaymentMethodDetailsAsync(PaymentMethodDetails details);

        Task<bool> UpdatePaymentTransactionAsync(PaymentTransaction transaction);
        Task<bool> DeletePaymentTransactionAsync(int transactionId);
    }

}