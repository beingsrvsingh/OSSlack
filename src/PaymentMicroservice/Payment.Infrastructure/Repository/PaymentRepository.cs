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
            return await _context.Set<PaymentTransaction>()
                .Include(t => t.PaymentMethod)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<PaymentTransaction>> GetPaymentsByUserIdAsync(string userId)
        {
            return await _context.Set<PaymentTransaction>()
                .Include(t => t.PaymentMethod)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PaymentTransactionLog>> GetTransactionLogsAsync(int paymentTransactionId)
        {
            return await _context.Set<PaymentTransactionLog>()
                .Where(log => log.PaymentTransactionId == paymentTransactionId)
                .OrderByDescending(log => log.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<RefundTransaction>> GetRefundsByTransactionIdAsync(int paymentTransactionId)
        {
            return await _context.Set<RefundTransaction>()
                .Where(r => r.PaymentTransactionId == paymentTransactionId)
                .ToListAsync();
        }

        public async Task<PaymentMethodDetails?> GetPaymentMethodByIdAsync(int id)
        {
            return await _context.Set<PaymentMethodDetails>().FindAsync(id);
        }

        public async Task AddPaymentTransactionAsync(PaymentTransaction transaction)
        {
            await _context.Set<PaymentTransaction>().AddAsync(transaction);
        }

        public async Task AddTransactionLogAsync(PaymentTransactionLog log)
        {
            await _context.Set<PaymentTransactionLog>().AddAsync(log);
        }

        public async Task AddRefundTransactionAsync(RefundTransaction refund)
        {
            await _context.Set<RefundTransaction>().AddAsync(refund);
        }

        public async Task AddPaymentMethodDetailsAsync(PaymentMethodDetails details)
        {
            await _context.Set<PaymentMethodDetails>().AddAsync(details);
        }

        public void UpdatePaymentTransaction(PaymentTransaction transaction)
        {
            _context.Set<PaymentTransaction>().Update(transaction);
        }

        public void DeletePaymentTransaction(PaymentTransaction transaction)
        {
            _context.Set<PaymentTransaction>().Remove(transaction);
        }
    }

}