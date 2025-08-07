using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface IPoojaKitItemMasterRepository : IRepository<PoojaKitItemMaster>
    {
        Task<IEnumerable<PoojaKitItemMaster>> GetByKitIdAsync(int poojaKitId);

    }
}