using Product.Domain.Entities;

namespace Product.Application.Services
{
    public interface IProductService
    {
        Task<ProductMaster?> GetProductWithVariantsAsync(int productId);
        Task<List<ProductMaster>> GetProductBySubCategoryIdAsync(int subCategoryId);
        Task<List<ProductMaster>> GetProductByProductNameAsync(string prodName);
        Task<IEnumerable<ProductRegionPriceMaster>> GetRegionPricesAsync(int productId);
        Task<IEnumerable<ProductVariantMaster>> GetVariantsAsync(int productId);
        Task<IEnumerable<ProductAttributeMaster>> GetAttributesAsync(int productId);
        Task<IEnumerable<LocalizedProductInfoMaster>> GetLocalizedInfoAsync(int productId);
        Task<IEnumerable<ProductTagMaster>> GetTagsAsync(int productId);
        Task<ProductSEOInfoMaster?> GetSEOInfoAsync(int productId);

        Task<bool> AddProductAsync(ProductMaster product);
        Task<bool> UpdateProductAsync(ProductMaster product);
        Task<bool> DeleteProductAsync(int productId);
    }

}