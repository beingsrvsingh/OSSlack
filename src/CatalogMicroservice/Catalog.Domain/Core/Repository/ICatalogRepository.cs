using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface ICatalogRepository : IRepository<CategoryMaster> { }
}
