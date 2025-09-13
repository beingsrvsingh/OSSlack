using Microsoft.EntityFrameworkCore;
using Product.Application.Services;
using Product.Domain.Entities;
using Product.Domain.Repository;
using Shared.Application.Common.Contracts;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;

namespace Product.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILoggerService<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILoggerService<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ProductMaster?> GetProductWithVariantsAsync(int productId)
        {
            try
            {
                return await _productRepository.GetProductWithVariantsAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProductWithVariantsAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<IEnumerable<ProductRegionPriceMaster>> GetRegionPricesAsync(int productId)
        {
            try
            {
                return await _productRepository.GetRegionPricesAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetRegionPricesAsync: {ex.Message}", ex);
                return Enumerable.Empty<ProductRegionPriceMaster>();
            }
        }

        public async Task<IEnumerable<ProductVariantMaster>> GetVariantsAsync(int productId)
        {
            try
            {
                return await _productRepository.GetVariantsAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetVariantsAsync: {ex.Message}", ex);
                return Enumerable.Empty<ProductVariantMaster>();
            }
        }

        public async Task<IEnumerable<LocalizedProductInfoMaster>> GetLocalizedInfoAsync(int productId)
        {
            try
            {
                return await _productRepository.GetLocalizedInfoAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetLocalizedInfoAsync: {ex.Message}", ex);
                return Enumerable.Empty<LocalizedProductInfoMaster>();
            }
        }

        public async Task<IEnumerable<ProductTagMaster>> GetTagsAsync(int productId)
        {
            try
            {
                return await _productRepository.GetTagsAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTagsAsync: {ex.Message}", ex);
                return Enumerable.Empty<ProductTagMaster>();
            }
        }

        public async Task<ProductSEOInfoMaster?> GetSEOInfoAsync(int productId)
        {
            try
            {
                return await _productRepository.GetSEOInfoAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetSEOInfoAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<bool> AddProductAsync(ProductMaster product)
        {
            try
            {
                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddProductAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductMaster product)
        {
            try
            {
                await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateProductAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);

                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {productId} not found.");
                    return false;
                }

                await _productRepository.DeleteAsync(product);
                await _productRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteProductAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<List<ProductMaster>> GetProductBySubCategoryIdAsync(int subCategoryId)
        {
            try
            {
                var result = await _productRepository.GetAsync(
                    p => p.SubCategoryId == subCategoryId,
                    include: query => query.Include(p => p.Images).Include(p => p.AttributeValues));

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProductBySubCategoryIdAsync: {ex.Message}", ex);
                return new List<ProductMaster>();
            }
        }

        public async Task<List<ProductMaster>> GetProductByProductNameAsync(string prodName)
        {
            var result = await _productRepository.GetAsync(p => p.Name.Contains(prodName));
            return result.ToList();
        }

        public async Task<ProductMaster?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<ProductMaster?> GetProductWithAttributesAsync(int productId)
        {
            return await _productRepository.GetSingleAsync(
                p => p.Id == productId,
                include: query => query.Include(p => p.Images).Include(p => p.AttributeValues));
        }

        public async Task<List<ProductFilterRawResult>> GetFilteredProductsAsync(
        List<int> attributeIds, int pageNumber,
        int pageSize,
        string? sortBy = null,
        bool sortDescending = false)
        {
            try
            {
                var result = await _productRepository.GetFilteredProductsRawAsync(attributeIds, pageNumber, pageSize, sortBy, sortDescending);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting filtered products for category {AttributeIds}.", string.Join(',', attributeIds));
                return new List<ProductFilterRawResult>();
            }
        }

        public async Task<List<ProductMaster>> GetProductsByIdAndCategoryIdAsync(List<int> ids, int? cid)
        {
            try
            {
                var result = await _productRepository.GetProductsByIdAndCategoryIdAsync(ids, cid);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products for productId {ProductIds}.", string.Join(',', ids));
                return [];
            }
        }

        public async Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var (products, totalCount) = await _productRepository.SearchAsync(query, page, pageSize, cancellationToken);

                var resultDtos = products.Select(p => new SearchItemDto
                {
                    Pid = p.Id.ToString(),
                    Cid = p.CategoryId.ToString(),
                    Scid = p.SubcategoryId.ToString(),
                    Name = p.Name ?? "",
                    Cost = (double)(p.Price ?? 0),
                    ThumbnailUrl = p.ThumbnailUrl ?? "",                                        
                    CategoryType = "Product",
                    Quantity = 1,
                    Limit = 1,
                    Rating = 1,
                    Reviews = 10,
                    AttributeValues = p.AttributeValues ?? [],
                    SearchItemMeta = new SearchItemMeta
                    {
                        Score = p.Score,
                        MatchType = p.MatchType ?? "Partial",
                    }
                }).ToList();

                var normalizedQuery = query.Trim();

                bool isCatOrSubcatExact = products.Any(p =>
                    string.Equals(p.CategoryNameSnapshot?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(p.SubCategoryNameSnapshot?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                bool isNameExact = products.Any(p =>
                    string.Equals(p.Name?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                string matchType = isCatOrSubcatExact || isNameExact ? "Exact" : "Partial";

                bool enableFilters = isCatOrSubcatExact || products.Any(p =>
                (p.CategoryNameSnapshot?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (p.SubCategoryNameSnapshot?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false));

                var attributes = Enumerable.Empty<BaseCatalogAttributeDto>();                

                var filterMeta = new BaseSearchFilterMetadata
                {
                    TotalCount = totalCount,
                    HasMoreResults = page * pageSize < totalCount,
                    Score = products.FirstOrDefault()?.Score ?? 0,
                    MatchType = matchType,
                    EnableFilters = enableFilters,
                    Source = "Product"
                };

                var result = new ProductSearchRawResultDto()
                {
                    Results = resultDtos,
                    Filters = filterMeta
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for products. Query: '{Query}', Page: {Page}, PageSize: {PageSize}", query, page, pageSize);
                return new ProductSearchRawResultDto
                {
                    Results = new List<SearchItemDto>(),
                    Filters = new BaseSearchFilterMetadata
                    {
                        TotalCount = 0,
                        HasMoreResults = false,
                        Score = 0,
                        MatchType = "Partial",
                        EnableFilters = false,
                        Source = "Product"
                    }
                };
            }
        }

    }

}