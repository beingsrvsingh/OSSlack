using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using Priest.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Infrastructure.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _repository;
        private readonly ILoggerService<ScheduleService> _logger;

        public ScheduleService(IScheduleRepository repository, ILoggerService<ScheduleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result> CreateScheduleAsync(CreateScheduleCommand command)
        {
            try
            {
                var schedule = new Schedule
                {
                    PriestId = command.PriestId,
                    DayOfWeek = command.Day,
                    Date = command.Date,
                };

                await _repository.AddAsync(schedule);
                return Result.Success("Schedule created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create schedule.");
                return Result.Failure("Unable to create schedule.");
            }
        }

        public async Task<Result> UpdateScheduleAsync(UpdateScheduleCommand command)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(command.Id);
                if (existing == null)
                    return Result.Failure("Schedule not found.");

                existing.DayOfWeek = command.Day;
                existing.Date = command.Date;

                await _repository.UpdateAsync(existing);
                return Result.Success("Schedule updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update schedule.");
                return Result.Failure("Unable to update schedule.");
            }
        }

        public async Task<Result> DeleteScheduleAsync(int id)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                    return Result.Failure("Schedule not found.");

                await _repository.DeleteAsync(existing);
                return Result.Success("Schedule deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete schedule.");
                return Result.Failure("Unable to delete schedule.");
            }
        }
    }

}
