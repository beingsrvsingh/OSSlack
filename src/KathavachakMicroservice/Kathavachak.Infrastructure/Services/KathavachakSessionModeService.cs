using Kathavachak.Application.Services;
using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Kathavachak.Infrastructure.Services
{
    public class KathavachakSessionModeService : IKathavachakSessionModeService
    {
        private readonly IKathavachakSessionModeRepository _repository;
        private readonly ILogger<KathavachakSessionModeService> _logger;

        public KathavachakSessionModeService(
            IKathavachakSessionModeRepository repository,
            ILogger<KathavachakSessionModeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<KathavachakSessionMode>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync");
                return Enumerable.Empty<KathavachakSessionMode>();
            }
        }

        public async Task<KathavachakSessionMode?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Id={id}");
                return null;
            }
        }

        public async Task<bool> CreateAsync(KathavachakSessionMode entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateAsync");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(KathavachakSessionMode entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    return false;

                await _repository.DeleteAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync");
                return false;
            }
        }
    }

}
