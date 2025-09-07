using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using Priest.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Infrastructure.Services
{
    public class PriestExpertiseService : IPriestExpertiseService
    {
        private readonly IPriestExpertiseRepository _repository;
        private readonly ILoggerService<PriestExpertiseService> _logger;

        public PriestExpertiseService(IPriestExpertiseRepository repository, ILoggerService<PriestExpertiseService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result> CreatePriestExpertiseAsync(CreatePriestExpertiseCommand command)
        {
            try
            {
                var expertise = new PriestExpertise
                {
                    PriestId = command.PriestId,
                    ExpertiseArea = command.ExpertiseArea
                };

                await _repository.AddAsync(expertise);
                return Result.Success("Priest expertise created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create priest expertise.");
                return Result.Failure("Unable to create priest expertise.");
            }
        }

        public async Task<Result> UpdatePriestExpertiseAsync(UpdatePriestExpertiseCommand command)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(command.Id);
                if (existing == null)
                    return Result.Failure("Priest expertise not found.");

                existing.ExpertiseArea = command.ExpertiseArea;

                await _repository.UpdateAsync(existing);
                return Result.Success("Priest expertise updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update priest expertise.");
                return Result.Failure("Unable to update priest expertise.");
            }
        }

        public async Task<Result> DeletePriestExpertiseAsync(int id)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                    return Result.Failure("Priest expertise not found.");

                await _repository.DeleteAsync(existing);
                return Result.Success("Priest expertise deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete priest expertise.");
                return Result.Failure("Unable to delete priest expertise.");
            }
        }
    }

}
