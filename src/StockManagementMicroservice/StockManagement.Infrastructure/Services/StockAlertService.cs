using Mapster;
using Shared.Application.Interfaces.Logging;
using StockManagement.Application.Contracts;
using StockManagement.Application.Services;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Services
{
    public class StockAlertService : IStockAlertService
    {
        private readonly IStockAlertRepository _repository;
        private readonly ILoggerService<StockAlertService> _logger;

        public StockAlertService(IStockAlertRepository repository, ILoggerService<StockAlertService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<StockAlertDto>> GetActiveAlertsAsync()
        {
            var alerts = await _repository.GetActiveAlertsAsync();
            return alerts.Adapt<IEnumerable<StockAlertDto>>();
        }

        public async Task<bool> AddAlertAsync(StockAlertDto dto)
        {
            try
            {
                var entity = dto.Adapt<StockAlert>();
                await _repository.AddAlertAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding stock alert", ex);
                return false;
            }
        }
    }

}