using Product.Application.Contracts;
using Product.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;

namespace Product.Application.Services
{
    public interface IProductService
    {
        Task<ProductMaster?> GetProductWithVariantsAsync(int productId);
        Task<List<TrendingProductResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int topN = 5);
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

        Task<List<ProductFilterRawResult>> GetFilteredProductsAsync(
        List<int>attributeIds, int pageNumber,
        int pageSize,
        string? sortBy = null,
        bool sortDescending = false);

        Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }

}