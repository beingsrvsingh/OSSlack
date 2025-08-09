using Mapster;
using Shared.Application.Interfaces.Logging;
using StockManagement.Application.Contracts;
using StockManagement.Application.Services;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _repository;
        private readonly ILoggerService<StockService> _logger;

        public StockService(IStockRepository repository, ILoggerService<StockService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<StockDto?> GetByIdAsync(int id)
        {
            var stock = await _repository.GetByIdAsync(id);
            return stock?.Adapt<StockDto>();
        }

        public async Task<IEnumerable<StockDto>> GetByWarehouseIdAsync(int warehouseId)
        {
            var stocks = await _repository.GetByWarehouseIdAsync(warehouseId);
            return stocks.Adapt<IEnumerable<StockDto>>();
        }

        public async Task<bool> AddStockAsync(StockDto stockDto)
        {
            try
            {
                var entity = stockDto.Adapt<StockMaster>();
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("AddStock failed", ex);
                return false;
            }
        }

        public async Task<bool> UpdateStockAsync(StockDto stockDto)
        {
            try
            {
                var entity = stockDto.Adapt<StockMaster>();
                await _repository.UpdateAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateStock failed", ex);
                return false;
            }
        }
    }

}