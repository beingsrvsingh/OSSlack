using Microsoft.EntityFrameworkCore;
using Product.Application.Services;
using Product.Domain.Entities;
using Product.Domain.Repository;
using Product.Infrastructure.Persistence.Catalog.Queries;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Product.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly ILoggerService<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILoggerService<ProductService> logger)
        {
            repository = productRepository;
            _logger = logger;
        }

        public async Task<List<ProductMaster>> GetProductByProductNameAsync(string prodName)
        {
            var result = await repository.GetAsync(p => p.Name.Contains(prodName));
            return result.ToList();
        }

        public async Task<ProductMaster?> GetProductByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<ProductMaster?> GetProductWithVariantsAsync(int productId)
        {
            try
            {
                return await repository.GetProductWithVariantsAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProductWithVariantsAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10)
        {
            List<ProductMaster> lstProducts = new List<ProductMaster> ();

            lstProducts = await repository.GetAsync((p) => (subCategoryId == null || p.CategoryId == subCategoryId) || p.IsTrending == true);

            var trendingProducts = lstProducts
                                    .Skip(pageNumber)
                                    .Take(pageSize)
                                    .Select(product =>
                                    {
                                        return new TrendingResponse
                                        {
                                            Id = product.Id.ToString(),
                                            Scid = product.SubCategoryId.ToString(),
                                            Name = product.Name
                                        };
                                    })
                                    .ToList();


            return trendingProducts;
        }

        public async Task<PagedResult<CatalogResponseDto>> GetTrendingProdcutsAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var queryable = repository.Query();

                var totalCount = await queryable.CountAsync();

                var skip = (pageNumber - 1) * pageSize;

                var products = await queryable
                                .AsNoTracking()
                                .Skip(skip)
                                .Take(pageSize)
                                .Where((p) => p.IsTrending == true)
                                .Select(CatalogQueries.ToCatalogResponse)
                                .ToListAsync();

                return new PagedResult<CatalogResponseDto>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    Items = products
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for products. Page: {Page}, PageSize: {PageSize}", pageNumber, pageSize);
                return new PagedResult<CatalogResponseDto>();
            }
        }

        public async Task<IEnumerable<ProductRegionPriceMaster>> GetRegionPricesAsync(int productId)
        {
            try
            {
                return await repository.GetRegionPricesAsync(productId);
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
                return await repository.GetVariantsAsync(productId);
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
                return await repository.GetLocalizedInfoAsync(productId);
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
                return await repository.GetTagsAsync(productId);
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
                return await repository.GetSEOInfoAsync(productId);
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
                await repository.AddAsync(product);
                await repository.SaveChangesAsync();
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
                await repository.UpdateAsync(product);
                await repository.SaveChangesAsync();
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
                var product = await repository.GetByIdAsync(productId);

                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {productId} not found.");
                    return false;
                }

                await repository.DeleteAsync(product);
                await repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteProductAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<List<CatalogResponseDto>?> GetProductBySubCategoryIdAsync(int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                // Use IQueryable from repository
                var query = repository.Query();

                if (subCategoryId.HasValue && subCategoryId.Value > 0)
                {
                    query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
                }

                var queryable = repository.Query();

                var products = await queryable
                                    .AsNoTracking()
                                    .Select(CatalogQueries.ToCatalogResponse)
                                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetProductBySubCategoryIdAsync");
                return new List<CatalogResponseDto>();
            }
        }

        public async Task<CatalogResponseDto?> GetProductWithAttributesAsync(int productId)
        {
            // Use IQueryable from repository
            var queryable = repository.Query();

            var productDto = await queryable
                                .AsNoTracking()
                                .Where(p => p.Id == productId)
                                .Select(CatalogQueries.ToCatalogResponse)
                                .FirstOrDefaultAsync();

            return productDto;
        }

        public async Task<List<CatalogResponseDto>> GetFilteredProductsAsync(List<int> attributeIds,int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
        {
            var queryable = repository.Query();

            if (subCategoryId.HasValue && subCategoryId.Value > 0)
            {
                queryable = queryable.Where(p => p.SubCategoryId == subCategoryId.Value);
            }

            if (attributeIds != null && attributeIds.Any())
            {
                // Ensure product has all selected attribute IDs
                queryable = queryable.Where(v => v.VariantMasters.Any(vm => attributeIds.All(atr => vm.Attributes.Any(attr => attr.CatalogAttributeValueId == atr))));

            }

            var totalCount = await queryable.CountAsync();

            var skip = (pageNumber - 1) * pageSize;

            var products = await queryable
                                .AsNoTracking()
                                .Skip(skip)
                                .Take(pageSize)
                                .Select(CatalogQueries.ToCatalogResponse)
                                .ToListAsync();

            return products;
        }

        public async Task<List<ProductMaster>> GetProductsByIdAndCategoryIdAsync(List<int> ids, int? cid)
        {
            try
            {
                var result = await repository.GetProductsByIdAndCategoryIdAsync(ids, cid);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products for productId {ProductIds}.", string.Join(',', ids));
                return [];
            }
        }

        public async Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = repository.Query();

                queryable = CatalogQueries.ApplySearch(queryable, query);

                var totalCount = await queryable.CountAsync();

                var skip = (pageNumber - 1) * pageSize;

                var products = await queryable
                                .AsNoTracking()
                                .Skip(skip)
                                .Take(pageSize)
                                .Select(CatalogQueries.ToCatalogResponse)
                                .ToListAsync();

                return new PagedResult<CatalogResponseDto>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    Items = products
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for products. Query: '{Query}', Page: {Page}, PageSize: {PageSize}", query, pageNumber, pageSize);
                return new PagedResult<CatalogResponseDto>();
            }
        }

    }

}