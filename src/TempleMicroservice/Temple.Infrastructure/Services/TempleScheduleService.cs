using Shared.Application.Interfaces.Logging;
using Temple.Application.Services;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Services
{
    public class TempleScheduleService : ITempleScheduleService
    {
        private readonly ITempleScheduleRepository _scheduleRepository;
        private readonly ILoggerService<TempleScheduleService> _logger;

        public TempleScheduleService(ITempleScheduleRepository scheduleRepository, ILoggerService<TempleScheduleService> logger)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Schedule schedule)
        {
            try
            {
                await _scheduleRepository.AddAsync(schedule);
                await _scheduleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Schedule schedule)
        {
            try
            {
                await _scheduleRepository.UpdateAsync(schedule);
                await _scheduleRepository.SaveChangesAsync();
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
                var entity = await _scheduleRepository.GetByIdAsync(id);
                if (entity == null) return false;

                await _scheduleRepository.DeleteAsync(entity);
                await _scheduleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<Schedule?> GetByIdAsync(int id)
        {
            return await _scheduleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            return await _scheduleRepository.GetAllAsync();
        }
    }

}
