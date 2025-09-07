using Shared.Application.Interfaces.Logging;
using Temple.Application.Services;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Services
{
    public class TempleLocalizedTextService : ITempleLocalizedTextService
    {
        private readonly ITempleLocalizedTextRepository _textRepository;
        private readonly ILoggerService<TempleLocalizedTextService> _logger;

        public TempleLocalizedTextService(ITempleLocalizedTextRepository textRepository, ILoggerService<TempleLocalizedTextService> logger)
        {
            _textRepository = textRepository;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(TempleLocalizedText text)
        {
            try
            {
                await _textRepository.AddAsync(text);
                await _textRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TempleLocalizedText text)
        {
            try
            {
                await _textRepository.UpdateAsync(text);
                await _textRepository.SaveChangesAsync();
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
                var entity = await _textRepository.GetByIdAsync(id);
                if (entity == null) return false;

                await _textRepository.DeleteAsync(entity);
                await _textRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<TempleLocalizedText?> GetByIdAsync(int id)
        {
            return await _textRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TempleLocalizedText>> GetAllAsync()
        {
            return await _textRepository.GetAllAsync();
        }
    }

}
