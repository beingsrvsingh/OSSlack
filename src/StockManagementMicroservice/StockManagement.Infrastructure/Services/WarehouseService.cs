using Mapster;
using Shared.Application.Interfaces.Logging;
using StockManagement.Application.Contracts;
using StockManagement.Application.Services;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;
        private readonly ILoggerService<WarehouseService> _logger;

        public WarehouseService(IWarehouseRepository repository, ILoggerService<WarehouseService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<WarehouseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Adapt<IEnumerable<WarehouseDto>>();
        }

        public async Task<WarehouseDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity?.Adapt<WarehouseDto>();
        }

        public async Task<bool> AddAsync(WarehouseDto dto)
        {
            try
            {
                var entity = dto.Adapt<WarehouseMaster>();
                await _repository.AddWarehouseAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding warehouse", ex);
                return false;
            }
        }

        public async Task<IEnumerable<WarehouseTransferDto>> GetTransfersAsync(int warehouseId)
        {
            var transfers = await _repository.GetTransfersByWarehouseIdAsync(warehouseId);
            return transfers.Adapt<IEnumerable<WarehouseTransferDto>>();
        }

        public async Task<bool> AddTransferAsync(WarehouseTransferDto dto)
        {
            try
            {
                var entity = dto.Adapt<WarehouseTransfer>();
                await _repository.AddTransferAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding transfer", ex);
                return false;
            }
        }
    }

}