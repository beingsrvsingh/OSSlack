using PaymentMicroservice.Application.Services;
using PaymentMicroservice.Domain.Core.Repository;
using PaymentMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace PaymentMicroservice.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly ILoggerService<PaymentService> _logger;

        public PaymentService(IPaymentRepository repository, ILoggerService<PaymentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PaymentTransaction?> GetPaymentTransactionByIdAsync(int id)
        {
            try
            {
                return await _repository.GetPaymentTransactionByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPaymentTransactionByIdAsync", ex);
                return null;
            }
        }

        public async Task<IEnumerable<PaymentTransaction>> GetPaymentsByUserIdAsync(string userId)
        {
            try
            {
                return await _repository.GetPaymentsByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPaymentsByUserIdAsync", ex);
                return Enumerable.Empty<PaymentTransaction>();
            }
        }

        public async Task<IEnumerable<PaymentTransactionLog>> GetTransactionLogsAsync(int paymentTransactionId)
        {
            try
            {
                return await _repository.GetTransactionLogsAsync(paymentTransactionId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetTransactionLogsAsync", ex);
                return Enumerable.Empty<PaymentTransactionLog>();
            }
        }

        public async Task<IEnumerable<RefundTransaction>> GetRefundsByTransactionIdAsync(int paymentTransactionId)
        {
            try
            {
                return await _repository.GetRefundsByTransactionIdAsync(paymentTransactionId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetRefundsByTransactionIdAsync", ex);
                return Enumerable.Empty<RefundTransaction>();
            }
        }

        public async Task<PaymentMethodDetails?> GetPaymentMethodByIdAsync(int id)
        {
            try
            {
                return await _repository.GetPaymentMethodByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPaymentMethodByIdAsync", ex);
                return null;
            }
        }

        public async Task<bool> AddPaymentTransactionAsync(PaymentTransaction transaction)
        {
            try
            {
                await _repository.AddPaymentTransactionAsync(transaction);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddPaymentTransactionAsync", ex);
                return false;
            }
        }

        public async Task<bool> AddTransactionLogAsync(PaymentTransactionLog log)
        {
            try
            {
                await _repository.AddTransactionLogAsync(log);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddTransactionLogAsync", ex);
                return false;
            }
        }

        public async Task<bool> AddRefundTransactionAsync(RefundTransaction refund)
        {
            try
            {
                await _repository.AddRefundTransactionAsync(refund);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddRefundTransactionAsync", ex);
                return false;
            }
        }

        public async Task<bool> AddPaymentMethodDetailsAsync(PaymentMethodDetails details)
        {
            try
            {
                await _repository.AddPaymentMethodDetailsAsync(details);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddPaymentMethodDetailsAsync", ex);
                return false;
            }
        }

        public async Task<bool> UpdatePaymentTransactionAsync(PaymentTransaction transaction)
        {
            try
            {
                _repository.UpdatePaymentTransaction(transaction);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in UpdatePaymentTransactionAsync", ex);
                return false;
            }
        }

        public async Task<bool> DeletePaymentTransactionAsync(int transactionId)
        {
            try
            {
                var entity = await _repository.GetPaymentTransactionByIdAsync(transactionId);
                if (entity == null) return false;

                _repository.DeletePaymentTransaction(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in DeletePaymentTransactionAsync", ex);
                return false;
            }
        }
    }

}