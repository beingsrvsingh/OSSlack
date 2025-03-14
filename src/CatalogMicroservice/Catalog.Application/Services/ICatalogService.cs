using Catalog.Domain.Entities;

namespace Catalog.Application.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CategoryMaster>> GetAllCatalog();
    }
}
