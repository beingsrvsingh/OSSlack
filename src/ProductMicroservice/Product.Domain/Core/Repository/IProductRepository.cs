using Product.Domain.Entities;
using Shared.Domain.Repository;

namespace Product.Domain.Repository
{
    public interface IProductRepository : IRepository<ProductMaster>
    {
        Task<ProductMaster?> GetProductWithVariantsAsync(int productId);
        Task<IEnumerable<ProductRegionPriceMaster>> GetRegionPricesAsync(int productId);
        Task<IEnumerable<ProductVariantMaster>> GetVariantsAsync(int productId);
        Task<IEnumerable<ProductAttributeMaster>> GetAttributesAsync(int productId);
        Task<IEnumerable<LocalizedProductInfoMaster>> GetLocalizedInfoAsync(int productId);
        Task<IEnumerable<ProductTagMaster>> GetTagsAsync(int productId);
        Task<ProductSEOInfoMaster?> GetSEOInfoAsync(int productId);
    }
}