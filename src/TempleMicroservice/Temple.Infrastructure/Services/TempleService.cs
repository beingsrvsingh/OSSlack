using Shared.Application.Interfaces.Logging;
using Temple.Application.Services;
using Temple.Domain.Entities;
using Temple.Domain.Repositories;

namespace Temple.Infrastructure.Services
{
    public class TempleService : ITempleService
    {
        private readonly ITempleRepository _templeRepository;
        private readonly ILoggerService<TempleService> _logger;

        public TempleService(ITempleRepository templeRepository, ILoggerService<TempleService> logger)
        {
            _templeRepository = templeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _templeRepository.GetAllAsync(page, pageSize);
        }

        public async Task<TempleMaster?> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await _templeRepository.GetByIdWithDetailsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetByIdWithDetailsAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<bool> CreateAsync(TempleMaster temple)
        {
            try
            {
                await _templeRepository.AddAsync(temple);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TempleMaster temple)
        {
            try
            {
                await _templeRepository.UpdateAsync(temple);
                await _templeRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _templeRepository.GetByIdAsync(id);
                if (entity == null) return false;

                await _templeRepository.DeleteAsync(entity);
                await _templeRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }
    }

}
