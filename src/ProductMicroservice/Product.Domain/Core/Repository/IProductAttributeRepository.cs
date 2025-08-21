
using Product.Domain.Entities;
using Shared.Domain.Repository;

namespace Product.Domain.Core.Repository
{
    public interface IProductAttributeRepository : IRepository<ProductAttributeValue>
    {
        Task<IEnumerable<ProductAttributeValue>> GetByProductIdAsync(int productId);
    }

}