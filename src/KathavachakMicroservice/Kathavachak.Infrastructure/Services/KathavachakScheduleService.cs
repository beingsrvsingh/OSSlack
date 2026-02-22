using Kathavachak.Application.Services;
using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Kathavachak.Infrastructure.Services
{
    public class KathavachakScheduleService : IKathavachakScheduleService
    {
        private readonly IKathavachakScheduleRepository _repository;
        private readonly ILogger<KathavachakScheduleService> _logger;

        public KathavachakScheduleService(
            IKathavachakScheduleRepository repository,
            ILogger<KathavachakScheduleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync");
                return Enumerable.Empty<Schedule>();
            }
        }

        public async Task<Schedule?> GetByIdAsync(int id)
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

        public async Task<bool> CreateAsync(Schedule entity)
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

        public async Task<bool> UpdateAsync(Schedule entity)
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
