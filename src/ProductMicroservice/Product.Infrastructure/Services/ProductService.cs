using Microsoft.EntityFrameworkCore;
using Product.Application.Contracts;
using Product.Application.Services;
using Product.Domain.Entities;
using Product.Domain.Repository;
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
        List<int>attributeIds, int pageNumber,
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

    }

}