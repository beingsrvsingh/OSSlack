using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using PriestMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Infrastructure.Services
{
    public class ConsultationModeService : IConsultationModeService
    {
        private readonly IConsultationModeRepository _repository;
        private readonly ILoggerService<ConsultationModeService> _logger;

        public ConsultationModeService(IConsultationModeRepository repository, ILoggerService<ConsultationModeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<ConsultationMode>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve consultation modes.");
                return Enumerable.Empty<ConsultationMode>();
            }
        }

        public async Task<Result> CreateConsultationModeAsync(CreateConsultationModeCommand command)
        {
            try
            {
                var mode = new ConsultationMode
                {
                    ExpertiseId = command.ExpertieseId,
                    Mode = command.Mode,
                    ConsultationModeMasterId = command.ConsultationModeMasterId
                };

                await _repository.AddAsync(mode);
                return Result.Success("Consultation mode created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create consultation mode.");
                return Result.Failure("Unable to create consultation mode.");
            }
        }

        public async Task<Result> UpdateConsultationModeAsync(UpdateConsultationModeCommand command)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(command.Id);
                if (existing == null)
                    return Result.Failure("Consultation mode not found.");

                existing.Mode = command.Mode;

                await _repository.UpdateAsync(existing);
                return Result.Success("Consultation mode updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update consultation mode.");
                return Result.Failure("Unable to update consultation mode.");
            }
        }

        public async Task<Result> DeleteConsultationModeAsync(int id)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                    return Result.Failure("Consultation mode not found.");

                await _repository.DeleteAsync(existing);
                return Result.Success("Consultation mode deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete consultation mode.");
                return Result.Failure("Unable to delete consultation mode.");
            }
        }
    }

}
