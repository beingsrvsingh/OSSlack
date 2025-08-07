using Catalog.Domain.Core.Repository;
using Shared.Domain.UOW;

namespace Catalog.Domain.Core.UOW
{
    public interface IUnitOfWork : IBaseUnitOfWork, IDisposable
    {
        ICategoryRepository CatalogRepository { get; }
    }
}
