using Catalog.Domain.Entities;

namespace Catalog.Application.Services
{
    public interface IPoojaKitItemService
    {
        Task<IEnumerable<PoojaKitItemMaster>> GetItemsByKitIdAsync(int poojaKitId);
        Task<PoojaKitItemMaster?> GetItemByIdAsync(int id);
        Task<bool> CreateItemAsync(PoojaKitItemMaster item);
        Task<bool> UpdateItemAsync(PoojaKitItemMaster item);
        Task<bool> DeleteItemAsync(int id);

        Task<IEnumerable<PoojaKitItemLocalizedText>> GetLocalizedTextsAsync(int itemId);
        Task<bool> AddOrUpdateLocalizedTextAsync(PoojaKitItemLocalizedText localizedText);
    }

}