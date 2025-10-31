using Pooja.Domain.Entities;
using Shared.Domain.Repository;

namespace Pooja.Domain.Core.Repository
{
    public interface IPoojaMasterRepository : IRepository<PoojaMaster>
    {
        // Pooja CRUD
        Task<PoojaMaster?> GetByIdAsync(int id);
        Task<IEnumerable<PoojaMaster>> GetAllAsync();
        Task AddAsync(PoojaMaster pooja);
        Task UpdateAsync(PoojaMaster pooja);
        Task DeleteAsync(int id);

        // Optional: query by temple or priest
        Task<IEnumerable<PoojaMaster>> GetByTempleAsync(int templeId);
        Task<IEnumerable<PoojaMaster>> GetByPriestAsync(int priestId);
    }
}
