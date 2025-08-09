
using StockManagement.Application.Contracts;

namespace StockManagement.Application.Services
{
    public interface IStockTransactionService
    {
        Task<IEnumerable<StockTransactionDto>> GetByStockIdAsync(int stockId);
        Task<bool> AddAsync(StockTransactionDto dto);

        Task<IEnumerable<StockTransactionLogDto>> GetLogsAsync(int transactionId);
        Task<bool> AddLogAsync(StockTransactionLogDto dto);
    }

}