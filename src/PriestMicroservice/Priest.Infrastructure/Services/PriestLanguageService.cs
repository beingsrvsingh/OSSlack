using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using PriestMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Infrastructure.Services
{
    public class PriestLanguageService : IPriestLanguageService
    {
        private readonly IPriestLanguageRepository _repository;
        private readonly ILoggerService<PriestLanguageService> _logger;

        public PriestLanguageService(IPriestLanguageRepository repository, ILoggerService<PriestLanguageService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result> CreatePriestLanguageAsync(CreatePriestLanguageCommand command)
        {
            try
            {
                var language = new PriestLanguage
                {
                    PriestId = command.PriestId,
                    LanguageName = command.Language
                };

                await _repository.AddAsync(language);
                return Result.Success("Priest language created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create priest language.");
                return Result.Failure("Unable to create priest language.");
            }
        }

        public async Task<Result> UpdatePriestLanguageAsync(UpdatePriestLanguageCommand command)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(command.Id);
                if (existing == null)
                    return Result.Failure("Priest language not found.");

                existing.LanguageName = command.Language;

                await _repository.UpdateAsync(existing);
                return Result.Success("Priest language updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update priest language.");
                return Result.Failure("Unable to update priest language.");
            }
        }

        public async Task<Result> DeletePriestLanguageAsync(int id)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                    return Result.Failure("Priest language not found.");

                await _repository.DeleteAsync(existing);
                return Result.Success("Priest language deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete priest language.");
                return Result.Failure("Unable to delete priest language.");
            }
        }
    }

}
