using Shared.Application.Interfaces.Logging;
using Temple.Application.Services;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Services
{
    public class TemplePoojaService : ITemplePoojaService
    {
        private readonly ITemplePoojaRepository _poojaRepository;
        private readonly ILoggerService<TemplePoojaService> _logger;

        public TemplePoojaService(ITemplePoojaRepository poojaRepository, ILoggerService<TemplePoojaService> logger)
        {
            _poojaRepository = poojaRepository;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(TemplePooja pooja)
        {
            try
            {
                await _poojaRepository.AddAsync(pooja);
                await _poojaRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TemplePooja pooja)
        {
            try
            {
                await _poojaRepository.UpdateAsync(pooja);
                await _poojaRepository.SaveChangesAsync();
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
                var entity = await _poojaRepository.GetByIdAsync(id);
                if (entity == null) return false;

                await _poojaRepository.DeleteAsync(entity);
                await _poojaRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<TemplePooja?> GetByIdAsync(int id)
        {
            return await _poojaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TemplePooja>> GetAllAsync()
        {
            return await _poojaRepository.GetAllAsync();
        }
    }

}
