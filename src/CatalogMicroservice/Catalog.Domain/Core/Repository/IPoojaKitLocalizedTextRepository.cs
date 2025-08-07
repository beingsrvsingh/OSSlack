using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface IPoojaKitLocalizedTextRepository : IRepository<PoojaKitLocalizedText>
    {
        Task<IEnumerable<PoojaKitLocalizedText>> GetByKitIdAsync(int poojaKitId);

    }
}