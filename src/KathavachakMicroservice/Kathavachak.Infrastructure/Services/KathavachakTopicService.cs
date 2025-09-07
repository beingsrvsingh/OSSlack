using Kathavachak.Application.Services;
using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Kathavachak.Infrastructure.Services
{
    public class KathavachakTopicService : IKathavachakTopicService
    {
        private readonly IKathavachakTopicRepository _repository;
        private readonly ILoggerService<KathavachakTopicService> _logger;

        public KathavachakTopicService(IKathavachakTopicRepository repository, ILoggerService<KathavachakTopicService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<KathavachakTopic>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllAsync: {ex.Message}", ex);
                return Enumerable.Empty<KathavachakTopic>();
            }
        }

        public async Task<KathavachakTopic?> GetByIdAsync(int id)
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

        public async Task<bool> CreateAsync(KathavachakTopic entity)
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

        public async Task<bool> UpdateAsync(KathavachakTopic entity)
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
                    _logger.LogWarning($"KathavachakTopic with ID {id} not found.");
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
