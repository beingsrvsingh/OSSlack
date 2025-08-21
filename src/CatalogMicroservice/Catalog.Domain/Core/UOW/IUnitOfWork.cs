using Catalog.Domain.Core.Repository;
using Shared.Domain.UOW;

namespace Catalog.Domain.Core.UOW
{
    public interface IUnitOfWork : IBaseUnitOfWork, IDisposable
    {
        ICategoryRepository CatalogRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        IPoojaKitItemRepository PoojaKitItemRepository { get; }
        ICatalogAttributeRepository CatalogAttributeRepository { get; }
        ICatalogAttributeAllowedValueRepository CatalogAttributeAllowedValueRepository{ get; }
    }
}
