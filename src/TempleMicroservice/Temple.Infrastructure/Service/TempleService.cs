using Temple.Application.Features.Commands;
using Temple.Application.Service;
using Temple.Domain.Entities;
using Temple.Domain.Entities.Enums;
using Temple.Domain.Repositories;
using Mapster;
using Shared.Application.Interfaces.Logging;

namespace Temple.Infrastructure.Service
{

    public class TempleService : ITempleService
    {
        private readonly ITempleRepository _repository;
        private readonly ILoggerService<TempleService> _logger;

        public TempleService(ITempleRepository repository, ILoggerService<TempleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<TempleMaster>> GetAvailableAsync(DateTime date, string language, string expertise)
        {
            try
            {
                return await _repository.GetAvailableAsync(date, language, expertise);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch available astrologers.");
                // Return null on error so handler can return failure Result
                return Enumerable.Empty<TempleMaster>();
            }
        }

        public async Task<TempleMaster?> GetByIdAsync(int astrologerId)
        {
            _logger.LogInfo($"Getting astrologer by Id: {astrologerId}");
            try
            {
                var astrologer = await _repository.GetByIdAsync(astrologerId);
                if (astrologer == null)
                    _logger.LogWarning($"Astrologer with Id {astrologerId} not found.");
                return astrologer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving astrologer with Id {astrologerId}", ex);
                throw;
            }
        }

        public async Task<TempleMaster?> GetByUserIdAsync(string userId)
        {
            _logger.LogInfo($"Getting astrologer by UserId: {userId}");
            try
            {
                var astrologer = await _repository.GetByIdAsync(userId);
                if (astrologer == null)
                    _logger.LogWarning($"Astrologer with UserId {userId} not found.");
                return astrologer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving astrologer with UserId {userId}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            _logger.LogInfo($"Getting all astrologers - Page: {page}, PageSize: {pageSize}");
            try
            {
                return await _repository.GetAllAsync(page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving astrologers", ex);
                throw;
            }
        }

        public async Task<bool> CreateAsync(CreateTempleCommand command)
        {
            _logger.LogInfo("Creating new astrologer");

            try
            {
                // Check if astrologer exists by unique field, e.g. Email
                var exists = await _repository.AnyAsync(a => a.UserId == command.UserId);
                if (exists)
                {
                    _logger.LogWarning($"Astrologer with userId {command.UserId} already exists.");
                    return false; // or you can throw, or handle as needed
                }

                // Create new astrologer
                var astrologer = command.Adapt<TempleMaster>();

                astrologer.AstrologerLanguages.Clear();
                foreach (var languageEnum in command.Languages)
                {
                    astrologer.AstrologerLanguages.Add(new AstrologerLanguage
                    {
                        LanguageId = (int)languageEnum
                    });
                }

                astrologer.TempleExpertises.Clear();
                foreach (var expertise in command.Expertise)
                {
                    astrologer.TempleExpertises.Add(new TempleExpertise
                    {
                        ExpertiseId = (int)expertise
                    });
                }

                await _repository.AddAsync(astrologer);
                await _repository.SaveChangesAsync();

                _logger.LogInfo($"Astrologer with userId {command.UserId} created successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating astrologer", ex);
                return false;
            }
        }


        public async Task<bool> UpdateAsync(UpdateTempleCommand command)
        {
            _logger.LogInfo($"Updating astrologer with Id {command.Id}");

            try
            {
                var astrologer = await _repository.GetAstrologerWithLanguagesAndExpertisesAsync(command.Id);
                if (astrologer == null)
                {
                    _logger.LogWarning($"Astrologer with Id {command.Id} not found.");
                    return false;
                }

                // Update scalar properties
                astrologer.Adapt(command);

                // Clear existing languages and insert new ones
                astrologer.AstrologerLanguages.Clear();
                foreach (var langEnum in command.Languages)
                {
                    astrologer.AstrologerLanguages.Add(new AstrologerLanguage
                    {
                        LanguageId = (int)langEnum
                    });
                }

                // Clear existing expertises and insert new ones
                astrologer.TempleExpertises.Clear();
                foreach (var expEnum in command.Expertise)
                {
                    astrologer.TempleExpertises.Add(new TempleExpertise
                    {
                        ExpertiseId = (int)expEnum
                    });
                }

                await _repository.UpdateAsync(astrologer);
                await _repository.SaveChangesAsync();

                _logger.LogInfo($"Astrologer with Id {command.Id} updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating astrologer with Id {command.Id}: {ex.Message}", ex);
                return false;
            }
        }



        public async Task<bool> DeleteAsync(int astrologerId)
        {
            _logger.LogInfo($"Deleting astrologer with Id: {astrologerId}");
            try
            {
                var astrologer = await _repository.GetAstrologerWithLanguagesAndExpertisesAsync(astrologerId);
                if (astrologer is null)
                {
                    _logger.LogInfo($"astrologer not found with Id: {astrologerId}");
                    return false;   
                }
                await _repository.DeleteAsync(astrologer);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting astrologer with Id {astrologerId}", ex);
                throw;
            }
        }

        public async Task<bool> SetLanguagesAsync(int astrologerId, IEnumerable<int> languageIds)
        {
            _logger.LogInfo($"Setting languages for astrologer Id: {astrologerId}");
            try
            {
                await _repository.SetLanguagesAsync(astrologerId, languageIds);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error setting languages for astrologer Id {astrologerId}", ex);
                throw;
            }
        }

        public async Task<bool> SetExpertisesAsync(int astrologerId, IEnumerable<int> expertiseIds)
        {
            _logger.LogInfo($"Setting expertises for astrologer Id: {astrologerId}");
            try
            {
                await _repository.SetExpertisesAsync(astrologerId, expertiseIds);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error setting expertises for astrologer Id {astrologerId}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<TempleMaster>> SearchAsync(string? language = null, string? expertise = null, ConsultationMode? consultationMode = null, bool? isActive = true, int page = 1, int pageSize = 20)
        {
            _logger.LogInfo($"Searching astrologers with language: {language}, expertise: {expertise}, consultationMode: {consultationMode}, isActive: {isActive}, page: {page}, pageSize: {pageSize}");
            try
            {
                return await _repository.SearchAsync(language, expertise, consultationMode, isActive, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error searching astrologers", ex);
                throw;
            }
        }
    }

}