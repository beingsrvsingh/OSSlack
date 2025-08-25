using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Domain.Core.Repository;
using PaymentMicroservice.Domain.Entities;
using PaymentMicroservice.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace PaymentMicroservice.Infrastructure.Repository
{
    public class PaymentRepository : Repository<PaymentTransaction>, IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<PaymentTransaction?> GetPaymentTransactionByIdAsync(int id)
        {
            return await _context.PaymentTransactions
                .Include(t => t.PaymentMethod)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<PaymentTransaction>> GetPaymentsByUserIdAsync(string userId)
        {
            return await _context.PaymentTransactions
                .Include(t => t.PaymentMethod)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<PaymentTransaction?> GetPaymentTransactionByOrderIdAsync(int orderId)
        {
            var payment = await _context.PaymentTransactions
                .Include(t => t.PaymentMethodDetails)
                .FirstOrDefaultAsync(t => t.OrderId == orderId);

            return payment;
        }


        public async Task<IEnumerable<PaymentTransactionLog>> GetTransactionLogsAsync(int paymentTransactionId)
        {
            return await _context.PaymentTransactionLogs
                .Where(log => log.PaymentTransactionId == paymentTransactionId)
                .OrderByDescending(log => log.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<RefundTransaction>> GetRefundsByTransactionIdAsync(int paymentTransactionId)
        {
            return await _context.RefundTransactions
                .Where(r => r.PaymentTransactionId == paymentTransactionId)
                .ToListAsync();
        }

        public async Task<PaymentMethodDetails?> GetPaymentMethodByIdAsync(int id)
        {
            return await _context.PaymentMethodDetails.FindAsync(id);
        }

        public async Task AddPaymentTransactionAsync(PaymentTransaction transaction)
        {
            await _context.PaymentTransactions.AddAsync(transaction);
        }

        public async Task AddTransactionLogAsync(PaymentTransactionLog log)
        {
            await _context.PaymentTransactionLogs.AddAsync(log);
        }

        public async Task AddRefundTransactionAsync(RefundTransaction refund)
        {
            await _context.RefundTransactions.AddAsync(refund);
        }

        public async Task AddPaymentMethodDetailsAsync(PaymentMethodDetails details)
        {
            await _context.PaymentMethodDetails.AddAsync(details);
        }

        public void UpdatePaymentTransaction(PaymentTransaction transaction)
        {
            _context.PaymentTransactions.Update(transaction);
        }

        public void DeletePaymentTransaction(PaymentTransaction transaction)
        {
            _context.PaymentTransactions.Remove(transaction);
        }
    }

}