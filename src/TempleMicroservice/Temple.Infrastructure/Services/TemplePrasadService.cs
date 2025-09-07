using Shared.Application.Interfaces.Logging;
using Temple.Application.Services;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Services
{
    public class TemplePrasadService : ITemplePrasadService
    {
        private readonly ITemplePrasadRepository _prasadRepository;
        private readonly ILoggerService<TemplePrasadService> _logger;

        public TemplePrasadService(ITemplePrasadRepository prasadRepository, ILoggerService<TemplePrasadService> logger)
        {
            _prasadRepository = prasadRepository;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(TemplePrasad prasad)
        {
            try
            {
                await _prasadRepository.AddAsync(prasad);
                await _prasadRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TemplePrasad prasad)
        {
            try
            {
                await _prasadRepository.UpdateAsync(prasad);
                await _prasadRepository.SaveChangesAsync();
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
                var entity = await _prasadRepository.GetByIdAsync(id);
                if (entity == null) return false;

                await _prasadRepository.UpdateAsync(entity);
                await _prasadRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<TemplePrasad?> GetByIdAsync(int id)
        {
            return await _prasadRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TemplePrasad>> GetAllAsync()
        {
            return await _prasadRepository.GetAllAsync();
        }
    }

}
