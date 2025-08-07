using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface IPoojaKitMasterRepository : IRepository<PoojaKitMaster>
    {
        Task<IEnumerable<PoojaKitMaster>> GetBySubCategoryIdAsync(int subCategoryId);

    }
}