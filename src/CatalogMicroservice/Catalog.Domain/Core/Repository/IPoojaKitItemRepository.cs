using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface IPoojaKitItemRepository : IRepository<PoojaKitItemMaster>
    {
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PoojaKitItemLocalizedText>> GetLocalizationsAsync(int itemId);
        Task<PoojaKitItemLocalizedText> AddLocalizationAsync(PoojaKitItemLocalizedText localization);
    }
}