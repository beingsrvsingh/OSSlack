using Catalog.Domain.Entities;

namespace Catalog.Application.Services
{
    public interface IPoojaKitService
    {
        Task<IEnumerable<PoojaKitMaster>> GetAllPoojaKitsAsync();
        Task<PoojaKitMaster?> GetPoojaKitByIdAsync(int id);
        Task<bool> CreatePoojaKitAsync(PoojaKitMaster poojaKit);
        Task<bool> UpdatePoojaKitAsync(PoojaKitMaster poojaKit);
        Task<bool> DeletePoojaKitAsync(int id);

        Task<IEnumerable<PoojaKitLocalizedText>> GetLocalizedTextsAsync(int kitId);
        Task<bool> AddOrUpdateLocalizedTextAsync(PoojaKitLocalizedText localizedText);
    }

}