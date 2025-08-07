using Catalog.Application.Services;
using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Catalog.Infrastructure.Services
{
    public class PoojaKitService : IPoojaKitService
    {
        private readonly IPoojaKitMasterRepository _poojaKitRepository;
        private readonly IPoojaKitLocalizedTextRepository _localizedTextRepository;
        private readonly ILoggerService<PoojaKitService> _logger;

        public PoojaKitService(
            IPoojaKitMasterRepository poojaKitRepository,
            IPoojaKitLocalizedTextRepository localizedTextRepository,
            ILoggerService<PoojaKitService> logger)
        {
            _poojaKitRepository = poojaKitRepository;
            _localizedTextRepository = localizedTextRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PoojaKitMaster>> GetAllPoojaKitsAsync()
        {
            try
            {
                return await _poojaKitRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching all pooja kits", ex);
                return Enumerable.Empty<PoojaKitMaster>();
            }
        }

        public async Task<PoojaKitMaster?> GetPoojaKitByIdAsync(int id)
        {
            try
            {
                return await _poojaKitRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching pooja kit by id {id}", ex);
                return null;
            }
        }

        public async Task<bool> CreatePoojaKitAsync(PoojaKitMaster poojaKit)
        {
            try
            {
                await _poojaKitRepository.AddAsync(poojaKit);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating pooja kit", ex);
                return false;
            }
        }

        public async Task<bool> UpdatePoojaKitAsync(PoojaKitMaster poojaKit)
        {
            try
            {
                await _poojaKitRepository.UpdateAsync(poojaKit);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating pooja kit with id {poojaKit.Id}", ex);
                return false;
            }
        }

        public async Task<bool> DeletePoojaKitAsync(int id)
        {
            try
            {
                var entity = await _poojaKitRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning($"PoojaKit with id {id} not found for deletion");
                    return false;
                }
                await _poojaKitRepository.DeleteAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting pooja kit with id {id}", ex);
                return false;
            }
        }

        public async Task<IEnumerable<PoojaKitLocalizedText>> GetLocalizedTextsAsync(int kitId)
        {
            try
            {
                return await _localizedTextRepository.GetAsync(x => x.PoojaKitId == kitId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching localized texts for pooja kit id {kitId}", ex);
                return Enumerable.Empty<PoojaKitLocalizedText>();
            }
        }

        public async Task<bool> AddOrUpdateLocalizedTextAsync(PoojaKitLocalizedText localizedText)
        {
            try
            {
                var existing = await _localizedTextRepository.GetByIdAsync(localizedText.Id);
                if (existing == null)
                {
                    await _localizedTextRepository.AddAsync(localizedText);
                }
                else
                {
                    existing.LanguageCode = localizedText.LanguageCode;
                    existing.LocalizedName = localizedText.LocalizedName;
                    existing.LocalizedDescription = localizedText.LocalizedDescription;
                    await _localizedTextRepository.UpdateAsync(existing);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding/updating localized text for pooja kit", ex);
                return false;
            }
        }
    }

}