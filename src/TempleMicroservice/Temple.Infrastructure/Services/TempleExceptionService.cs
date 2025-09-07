using Shared.Application.Interfaces.Logging;
using Temple.Application.Services;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Services
{
    public class TempleExceptionService : ITempleExceptionService
    {
        private readonly ITempleExceptionRepository _exceptionRepository;
        private readonly ILoggerService<TempleExceptionService> _logger;

        public TempleExceptionService(ITempleExceptionRepository exceptionRepository, ILoggerService<TempleExceptionService> logger)
        {
            _exceptionRepository = exceptionRepository;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(TempleException exception)
        {
            try
            {
                await _exceptionRepository.AddAsync(exception);
                await _exceptionRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TempleException exception)
        {
            try
            {
                await _exceptionRepository.UpdateAsync(exception);
                await _exceptionRepository.SaveChangesAsync();
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
                var entity = await _exceptionRepository.GetByIdAsync(id);
                if (entity == null) return false;

                await _exceptionRepository.DeleteAsync(entity);
                await _exceptionRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<TempleException?> GetByIdAsync(int id)
        {
            return await _exceptionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TempleException>> GetAllAsync()
        {
            return await _exceptionRepository.GetAllAsync();
        }
    }

}
