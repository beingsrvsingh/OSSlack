using AstrologerMicroservice.Application.Service;
using AstrologerMicroservice.Domain.Core.Repository;
using AstrologerMicroservice.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace AstrologerMicroservice.Infrastructure.Services.Identity
{
    public class SeedService : ISeedService
    {
        private readonly ILanguageRepository languageRepository;
        private readonly IExpertiesRepository expertiesRepository;
        private readonly ILoggerService<SeedService> _logger;

        public SeedService(ILoggerService<SeedService> logger,
        ILanguageRepository languageRepository, IExpertiesRepository expertiesRepository)
        {
            _logger = logger;
            this.languageRepository = languageRepository;
            this.expertiesRepository = expertiesRepository;
        }

        public async Task<bool> SeedAstrologerExpertiesAsync()
        {
            try
            {
                var existingExpertise = await expertiesRepository.GetAllAsync();
                if (existingExpertise.Any()) return true;

                var seedExpertise = new List<Expertise>
                {
                    new() { Name = "Kundli" },
                    new() { Name = "Pooja" },
                    new() { Name = "Consultation" },
                    new() { Name = "Matchmaking" },
                    new() { Name = "Mind Reading" }
                };

                foreach (var expertise in seedExpertise)
                {
                    await expertiesRepository.AddAsync(expertise);
                }

                await expertiesRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to seed astrologer experties");
                return false;
            }
        }

        public async Task<bool> SeedAstrologerLanguagesAsync()
        {
            try
            {
                var existingLanguages = await languageRepository.GetAllAsync();
                if (existingLanguages.Any()) return true;

                var seedLanguages = new List<Language>
                {
                    new() { Name = "English" },
                    new() { Name = "Hindi" },
                    new() { Name = "Sanskrit" },
                    new() { Name = "Tamil" },
                    new() { Name = "Telugu" },
                    new() { Name = "Marathi" }
                };

                foreach (var lang in seedLanguages)
                {
                    await languageRepository.AddAsync(lang);
                }

                await languageRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to seed astrologer languages");
                return false;
            }
        }

    }
}
