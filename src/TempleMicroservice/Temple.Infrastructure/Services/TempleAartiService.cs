using Shared.Application.Interfaces.Logging;
using Temple.Application.Services;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Services
{
    public class TempleAartiService : ITempleAartiService
    {
        private readonly ITempleAartiRepository _aartiRepository;
        private readonly ILoggerService<TempleAartiService> _logger;

        public TempleAartiService(ITempleAartiRepository aartiRepository, ILoggerService<TempleAartiService> logger)
        {
            _aartiRepository = aartiRepository;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(TempleAarti aarti)
        {
            try
            {
                await _aartiRepository.AddAsync(aarti);
                await _aartiRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TempleAarti aarti)
        {
            try
            {
                await _aartiRepository.UpdateAsync(aarti);
                await _aartiRepository.SaveChangesAsync();
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
                var entity = await _aartiRepository.GetByIdAsync(id);
                if (entity == null) return false;

                await _aartiRepository.DeleteAsync(entity);
                await _aartiRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<TempleAarti?> GetByIdAsync(int id)
        {
            return await _aartiRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TempleAarti>> GetAllAsync()
        {
            return await _aartiRepository.GetAllAsync();
        }
    }

}
