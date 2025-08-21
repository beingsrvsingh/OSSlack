using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface ICatalogAttributeRepository : IRepository<CatalogAttribute>
    {
        Task<List<CatalogAttribute>> GetAttributesByCategoryIdAsync(int categoryId);
    }

}