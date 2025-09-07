using Kathavachak.Application.Services;
using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Kathavachak.Infrastructure.Services
{
    public class KathavachakCategoryService : IKathavachakCategoryService
    {
        private readonly IKathavachakCategoryRepository _repository;
        private readonly ILoggerService<KathavachakCategoryService> _logger;

        public KathavachakCategoryService(IKathavachakCategoryRepository repository, ILoggerService<KathavachakCategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<KathavachakCategory>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllAsync: {ex.Message}", ex);
                return Enumerable.Empty<KathavachakCategory>();
            }
        }

        public async Task<KathavachakCategory?> GetByIdAsync(int id)
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

        public async Task<bool> CreateAsync(KathavachakCategory entity)
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

        public async Task<bool> UpdateAsync(KathavachakCategory entity)
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
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning($"KathavachakCategory with ID {id} not found.");
                    return false;
                }
                await _repository.DeleteAsync(entity);
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
