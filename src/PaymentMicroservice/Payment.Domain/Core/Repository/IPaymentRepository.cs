using PaymentMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace PaymentMicroservice.Domain.Core.Repository
{
    public interface IPaymentRepository : IRepository<PaymentTransaction>
    {
        Task<PaymentTransaction?> GetPaymentTransactionByIdAsync(int id);
        Task<PaymentTransaction?> GetPaymentTransactionByOrderIdAsync(string orderId);
        Task<IEnumerable<PaymentTransaction>> GetPaymentsByUserIdAsync(string userId);

        Task<IEnumerable<PaymentTransactionLog>> GetTransactionLogsAsync(int paymentTransactionId);
        Task<IEnumerable<RefundTransaction>> GetRefundsByTransactionIdAsync(int paymentTransactionId);

        Task<PaymentMethodDetails?> GetPaymentMethodByIdAsync(int id);

        Task AddPaymentTransactionAsync(PaymentTransaction transaction);
        Task AddTransactionLogAsync(PaymentTransactionLog log);
        Task AddRefundTransactionAsync(RefundTransaction refund);
        Task AddPaymentMethodDetailsAsync(PaymentMethodDetails details);

        void UpdatePaymentTransaction(PaymentTransaction transaction);
        void DeletePaymentTransaction(PaymentTransaction transaction);
    }
}