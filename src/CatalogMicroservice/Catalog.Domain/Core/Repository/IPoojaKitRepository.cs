using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface IPoojaKitRepository : IRepository<PoojaKitMaster>
    {
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<PoojaKitLocalizedText>> GetLocalizationsAsync(int kitId);
        Task<PoojaKitLocalizedText> AddLocalizationAsync(PoojaKitLocalizedText localization);
    }
}