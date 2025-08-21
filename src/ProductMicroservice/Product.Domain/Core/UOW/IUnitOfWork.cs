using Product.Domain.Core.Repository;
using Product.Domain.Repository;
using Shared.Domain.UOW;

namespace Product.Domain.Core.UOW
{
    public interface IUnitOfWork : IBaseUnitOfWork, IDisposable
    {
        IProductRepository ProductRepository { get; }
        IProductAttributeRepository ProductAttributeRepository{ get; }
    }
}
