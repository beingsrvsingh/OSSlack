
using Shared.Domain.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Core.Repository
{
    public interface IStockTransactionRepository : IRepository<StockTransaction>
    {
        Task<IEnumerable<StockTransaction>> GetByStockIdAsync(int stockId);
        Task AddTransactionAsync(StockTransaction transaction);

        Task<IEnumerable<StockTransactionLog>> GetLogsByTransactionIdAsync(int transactionId);
        Task AddTransactionLogAsync(StockTransactionLog log);
    }

}