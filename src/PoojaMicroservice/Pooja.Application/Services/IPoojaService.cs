using Pooja.Domain.Entities;

namespace Pooja.Application.Services
{
    public interface IPoojaService
    {
        Task<PoojaMaster?> GetPoojaByIdAsync(int id);
        Task<IEnumerable<PoojaMaster>> GetAllPoojasAsync();
        Task<IEnumerable<PoojaMaster>> GetPoojasByTempleAsync(int templeId);
        Task<IEnumerable<PoojaMaster>> GetPoojasByPriestAsync(int priestId);
        Task AddPoojaAsync(PoojaMaster pooja);
        Task UpdatePoojaAsync(PoojaMaster pooja);
        Task DeletePoojaAsync(int id);

        // Optional: business-specific methods
        Task<IEnumerable<PoojaMaster>> SearchPoojasAsync(string keyword);
        Task<bool> IsPoojaAvailableAtHomeAsync(int poojaId);
    }

}
