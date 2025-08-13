using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface IPoojaKitItemRepository : IRepository<PoojaKitItemMaster>
    {
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PoojaKitItemMaster>> GetByKitItemIdAsync(int poojaKitItemId);
    }
}