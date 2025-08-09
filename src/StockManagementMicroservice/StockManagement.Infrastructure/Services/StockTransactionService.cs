using Mapster;
using Shared.Application.Interfaces.Logging;
using StockManagement.Application.Contracts;
using StockManagement.Application.Services;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Services
{
    public class StockTransactionService : IStockTransactionService
    {
        private readonly IStockTransactionRepository _repository;
        private readonly ILoggerService<StockTransactionService> _logger;

        public StockTransactionService(IStockTransactionRepository repository, ILoggerService<StockTransactionService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<StockTransactionDto>> GetByStockIdAsync(int stockId)
        {
            var result = await _repository.GetByStockIdAsync(stockId);
            return result.Adapt<IEnumerable<StockTransactionDto>>();
        }

        public async Task<bool> AddAsync(StockTransactionDto dto)
        {
            try
            {
                var entity = dto.Adapt<StockTransaction>();
                await _repository.AddTransactionAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding stock transaction", ex);
                return false;
            }
        }

        public async Task<IEnumerable<StockTransactionLogDto>> GetLogsAsync(int transactionId)
        {
            var logs = await _repository.GetLogsByTransactionIdAsync(transactionId);
            return logs.Adapt<IEnumerable<StockTransactionLogDto>>();
        }

        public async Task<bool> AddLogAsync(StockTransactionLogDto dto)
        {
            try
            {
                var entity = dto.Adapt<StockTransactionLog>();
                await _repository.AddTransactionLogAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding transaction log", ex);
                return false;
            }
        }
    }

}