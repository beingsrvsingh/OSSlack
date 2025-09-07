using Kathavachak.Application.Services;
using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Kathavachak.Infrastructure.Services
{
    public class KathavachakService : IKathavachakService
    {
        private readonly IKathavachakRepository _repository;
        private readonly ILoggerService<KathavachakService> _logger;

        public KathavachakService(IKathavachakRepository repository, ILoggerService<KathavachakService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<KathavachakMaster?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetByIdAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<IEnumerable<KathavachakMaster>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllAsync: {ex.Message}", ex);
                return Enumerable.Empty<KathavachakMaster>();
            }
        }

        public async Task<bool> CreateAsync(KathavachakMaster entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(KathavachakMaster entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                await _repository.SaveChangesAsync();
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
                var kathavachak = await _repository.GetByIdAsync(id);
                if(kathavachak == null)
                {
                    _logger.LogWarning($"Kathavachak with ID {id} not found for deletion.");
                    return false;
                }
                kathavachak.IsActive = false;
                await _repository.UpdateAsync(kathavachak);
                await _repository.SaveChangesAsync();
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
