using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface IPoojaKitItemLocalizedTextRepository : IRepository<PoojaKitItemLocalizedText>
    {
        Task<IEnumerable<PoojaKitItemLocalizedText>> GetByItemIdAsync(int itemId);

    }
}