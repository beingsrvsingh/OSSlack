using Mapster;
using Shared.Application.Interfaces.Logging;
using StockManagement.Application.Contracts;
using StockManagement.Application.Services;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Services
{
    public class StockAdjustmentService : IStockAdjustmentService
    {
        private readonly IStockAdjustmentRepository _repository;
        private readonly ILoggerService<StockAdjustmentService> _logger;

        public StockAdjustmentService(IStockAdjustmentRepository repository, ILoggerService<StockAdjustmentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<StockAdjustmentDto>> GetAdjustmentsAsync(int stockId)
        {
            var result = await _repository.GetByStockIdAsync(stockId);
            return result.Adapt<IEnumerable<StockAdjustmentDto>>();
        }

        public async Task<bool> AddAdjustmentAsync(StockAdjustmentDto dto)
        {
            try
            {
                var entity = dto.Adapt<StockAdjustment>();
                await _repository.AddAdjustmentAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding stock adjustment", ex);
                return false;
            }
        }

        public async Task<IEnumerable<StockReservationDto>> GetReservationsAsync(int stockId)
        {
            var result = await _repository.GetReservationsByStockIdAsync(stockId);
            var resultDto = result.Adapt<IEnumerable<StockReservationDto>>();
            return resultDto;
        }

        public async Task<bool> AddReservationAsync(Application.Contracts.StockReservationDto dto)
        {
            try
            {
                var entity = dto.Adapt<StockReservation>();
                await _repository.AddReservationAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding reservation", ex);
                return false;
            }
        }
    }


}