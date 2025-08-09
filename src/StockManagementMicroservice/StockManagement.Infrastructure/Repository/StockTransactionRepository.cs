using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence.Context;

namespace StockManagement.Infrastructure.Repository
{
    public class StockTransactionRepository : Repository<StockTransaction>, IStockTransactionRepository
    {
        private readonly StockDbContext _context;

        public StockTransactionRepository(StockDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<StockTransaction>> GetByStockIdAsync(int stockId) =>
            await _context.StockTransactions
                .Where(t => t.StockId == stockId)
                .ToListAsync();

        public async Task AddTransactionAsync(StockTransaction transaction) =>
            await _context.StockTransactions.AddAsync(transaction);

        public async Task<IEnumerable<StockTransactionLog>> GetLogsByTransactionIdAsync(int transactionId) =>
            await _context.StockTransactionLogs
                .Where(l => l.StockTransactionLogId == transactionId)
                .ToListAsync();

        public async Task AddTransactionLogAsync(StockTransactionLog log) =>
            await _context.StockTransactionLogs.AddAsync(log);
    }

}