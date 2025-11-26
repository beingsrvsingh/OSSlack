using Product.Application.Contracts;
using Product.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;

namespace Product.Application.Services
{
    public interface IProductService
    {
        Task<ProductMaster?> GetProductWithVariantsAsync(int productId);
        Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int topN = 5);
        Task<List<CatalogResponseDto>?> GetProductBySubCategoryIdAsync(int? subCategoryId, int topN = 5);
        Task<List<ProductMaster>> GetProductByProductNameAsync(string prodName);
        Task<IEnumerable<ProductRegionPriceMaster>> GetRegionPricesAsync(int productId);
        Task<IEnumerable<ProductVariantMaster>> GetVariantsAsync(int productId);
        Task<IEnumerable<LocalizedProductInfoMaster>> GetLocalizedInfoAsync(int productId);
        Task<IEnumerable<ProductTagMaster>> GetTagsAsync(int productId);
        Task<ProductSEOInfoMaster?> GetSEOInfoAsync(int productId);
        Task<ProductMaster?> GetProductByIdAsync(int id);
        Task<List<ProductMaster>> GetProductsByIdAndCategoryIdAsync(List<int> pids, int? cid);
        Task<bool> AddProductAsync(ProductMaster product);
        Task<bool> UpdateProductAsync(ProductMaster product);
        Task<bool> DeleteProductAsync(int productId);
        Task<CatalogResponseDto?> GetProductWithAttributesAsync(int productId);

        Task<List<CatalogResponseDto>> GetFilteredProductsAsync(List<int> attributeIds, int? subCategoryId = null, int topN = 10);

        Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }

}