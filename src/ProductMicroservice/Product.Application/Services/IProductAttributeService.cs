
using Product.Domain.Entities;

namespace Product.Application.Services
{
    public interface IProductAttributeService
    {
        Task<IEnumerable<ProductAttributeValue>> GetAttributesByProductIdAsync(int productId);
    }

}