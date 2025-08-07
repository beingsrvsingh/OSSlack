using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.Services;
using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Catalog.Infrastructure.Services
{
    public class PoojaKitItemService : IPoojaKitItemService
    {
        private readonly IPoojaKitItemMasterRepository _itemRepository;
        private readonly IPoojaKitItemLocalizedTextRepository _localizedTextRepository;
        private readonly ILoggerService<PoojaKitItemService> _logger;

        public PoojaKitItemService(
            IPoojaKitItemMasterRepository itemRepository,
            IPoojaKitItemLocalizedTextRepository localizedTextRepository,
            ILoggerService<PoojaKitItemService> logger)
        {
            _itemRepository = itemRepository;
            _localizedTextRepository = localizedTextRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PoojaKitItemMaster>> GetItemsByKitIdAsync(int poojaKitId)
        {
            try
            {
                return await _itemRepository.GetAsync(x=> x.PoojaKitMasterId == poojaKitId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching items for PoojaKitId {poojaKitId}", ex);
                return Enumerable.Empty<PoojaKitItemMaster>();
            }
        }

        public async Task<PoojaKitItemMaster?> GetItemByIdAsync(int id)
        {
            try
            {
                return await _itemRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching item by id {id}", ex);
                return null;
            }
        }

        public async Task<bool> CreateItemAsync(PoojaKitItemMaster item)
        {
            try
            {
                await _itemRepository.AddAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating pooja kit item", ex);
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(PoojaKitItemMaster item)
        {
            try
            {
                await _itemRepository.UpdateAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating pooja kit item with id {item.Id}", ex);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                var entity = await _itemRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning($"PoojaKitItem with id {id} not found for deletion");
                    return false;
                }
                await _itemRepository.DeleteAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting pooja kit item with id {id}", ex);
                return false;
            }
        }

        public async Task<IEnumerable<PoojaKitItemLocalizedText>> GetLocalizedTextsAsync(int itemId)
        {
            try
            {
                return await _localizedTextRepository.GetAsync(x => x.PoojaKitId == itemId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching localized texts for item id {itemId}", ex);
                return Enumerable.Empty<PoojaKitItemLocalizedText>();
            }
        }

        public async Task<bool> AddOrUpdateLocalizedTextAsync(PoojaKitItemLocalizedText localizedText)
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
                _logger.LogError("Error adding/updating localized text for pooja kit item", ex);
                return false;
            }
        }
    }


}