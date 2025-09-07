using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using Priest.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Infrastructure.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _repository;
        private readonly ILoggerService<TimeSlotService> _logger;

        public TimeSlotService(ITimeSlotRepository repository, ILoggerService<TimeSlotService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result> CreateTimeSlotAsync(CreateTimeSlotCommand command)
        {
            try
            {
                var slot = new TimeSlot
                {
                    ScheduleId = command.ScheduleId,
                    StartTime = command.StartTime,
                    EndTime = command.EndTime,
                    IsBooked = command.IsBooked
                };

                await _repository.AddAsync(slot);
                return Result.Success("Time slot created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create time slot.");
                return Result.Failure("Unable to create time slot.");
            }
        }

        public async Task<Result> UpdateTimeSlotAsync(UpdateTimeSlotCommand command)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(command.Id);
                if (existing == null)
                    return Result.Failure("Time slot not found.");

                existing.StartTime = command.StartTime;
                existing.EndTime = command.EndTime;
                existing.IsBooked = command.IsBooked;

                await _repository.UpdateAsync(existing);
                return Result.Success("Time slot updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update time slot.");
                return Result.Failure("Unable to update time slot.");
            }
        }

        public async Task<Result> DeleteTimeSlotAsync(int id)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                    return Result.Failure("Time slot not found.");

                await _repository.DeleteAsync(existing);
                return Result.Success("Time slot deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete time slot.");
                return Result.Failure("Unable to delete time slot.");
            }
        }
    }

}
