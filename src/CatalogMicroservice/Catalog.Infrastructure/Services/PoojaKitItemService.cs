using Catalog.Application.Services;
using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Catalog.Infrastructure.Services
{
    public class PoojaKitItemService : IPoojaKitItemService
    {
        private readonly IPoojaKitItemRepository _itemRepository;
        private readonly ILoggerService<PoojaKitItemService> _logger;

        public PoojaKitItemService(
            IPoojaKitItemRepository itemRepository,
            ILoggerService<PoojaKitItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PoojaKitItemMaster>> GetItemsByKitIdAsync(int poojaKitItemId)
        {
            try
            {
                return await _itemRepository.GetAsync(x=> x.Id == poojaKitItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching items for poojaKitItemId {poojaKitItemId}", ex);
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
    }


}