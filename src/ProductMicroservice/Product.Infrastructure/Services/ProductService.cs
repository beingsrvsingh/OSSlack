using Microsoft.EntityFrameworkCore;
using NLog.Filters;
using Product.Application.Contracts;
using Product.Application.Services;
using Product.Domain.Entities;
using Product.Domain.Repository;
using Shared.Application.Common.Contracts;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;
using Shared.Utilities.Response;
using System.Linq;

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

        public async Task<List<ProductMaster>> GetProductByProductNameAsync(string prodName)
        {
            var result = await _productRepository.GetAsync(p => p.Name.Contains(prodName));
            return result.ToList();
        }

        public async Task<ProductMaster?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
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

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int topN = 5)
        {
            List<ProductMaster> lstProducts = new List<ProductMaster> ();

            lstProducts = await _productRepository.GetAsync((p) => p.CategoryId == subCategoryId || p.IsTrending == true);

            var trendingProducts = lstProducts
                                    .Take(topN)
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

        public async Task<List<CatalogResponseDto>?> GetProductBySubCategoryIdAsync(int? subCategoryId = null, int topN = 5)
        {
            try
            {
                // Use IQueryable from repository
                var query = _productRepository.Query();

                if (subCategoryId.HasValue && subCategoryId.Value > 0)
                {
                    query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
                }

                var products = await query
                    .Take(topN)
                    .Select(p => new CatalogResponseDto
                    {
                        Id = p.Id.ToString(),
                        Name = p.Name,
                        ThumbnailUrl = p.ThumbnailUrl,
                        Rating = p.Rating,
                        Reviews = p.Reviews,
                        SubCategoryId = p.SubCategoryId.ToString(),
                        IsTrending = p.IsTrending,
                        IsFeatured = p.IsFeatured,
                        Price = new PriceResponseDto
                        {
                            Amount = p.Price.Amount,
                            Currency = p.Price.Currency,
                            Discount = p.Price.Discount,
                            Mrp = p.Price.Mrp,
                            Tax = p.Price.Tax
                        },

                        // Media
                        Media = p.ProductImages.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // addons
                        Addons = p.ProductAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,                            
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = p.Price.Amount,
                                Mrp = p.Price.Mrp
                            },
                        }).ToList(),

                        // attributes
                        Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                        {
                            Key = a.AttributeKey,
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String, 
                        }).ToList(),

                        // Variants
                        Variants = p.VariantMasters.Select(v => new CatalogVariantResponseDto
                        {
                            Id = v.Id.ToString(),
                            Name = v.Name,
                            Price = new PriceResponseDto { 
                                Amount = v.Price.Amount, 
                                Currency = v.Price.Currency, 
                                Discount = v.Price.Discount, 
                                Mrp = v.Price.Mrp, 
                                Tax = v.Price.Tax },
                            StockQuantity = v.StockQuantity,
                            Attributes = v.Attributes.AsEnumerable()
                                .GroupBy(a => a.AttributeGroupNameSnapshot)
                                .Select(g => new AttributeGroupResponseDto
                                {
                                    AttributeGroupName = g.Key,
                                    Attributes = g.Select(a => new AttributeResponseDto
                                    {
                                        Key = a.AttributeKey,
                                        Label = a.AttributeLabel ?? "",
                                        Value = a.Value,
                                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                                    }).ToList()
                                })
                                .ToList(),
                            Addons = v.ProductAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Price = new PriceResponseDto
                                {
                                    Amount = a.Price.Amount,
                                    Mrp = a.Price.Mrp
                                },
                            }).ToList(),
                            Media = v.VariantImages.Select(img => new MediaResponseDto
                            {
                                Url = img.ImageUrl,
                                Type = img.MediaType.ToString(),
                                AltText = img.AltText,
                                SortOrder = img.SortOrder
                            }).ToList()
                        }).ToList()
                    }).ToListAsync();

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
            var query = _productRepository.Query();

            var productDto = await query
                .Where(p => p.Id == productId)
                .Select(p => new CatalogResponseDto
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ThumbnailUrl = p.ThumbnailUrl,
                    Rating = p.Rating,
                    Reviews = p.Reviews,
                    SubCategoryId = p.SubCategoryId.ToString(),
                    Price = new PriceResponseDto
                    {
                        Amount = p.Price.Amount,
                        Currency = p.Price!.Currency,
                        Discount = p.Price.Discount,
                        Mrp = p.Price.Mrp,
                        Tax = p.Price.Tax
                    },
                    IsTrending = p.IsTrending,
                    IsFeatured = p.IsFeatured,

                    // Media
                    Media = p.ProductImages.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Product-level addons
                    Addons = p.ProductAddons.Select(a => new AddonResponseDto
                    {
                        Name = a.Name,
                        Description = a.Description,
                        Price = new PriceResponseDto
                        {
                            Amount = a.Price.Amount,
                            Mrp = a.Price.Mrp
                        },
                    }).ToList(),

                    // Product-level attributes
                    Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                    {
                        Key = a.AttributeKey,
                        Label = a.AttributeLabel ?? "",
                        Value = a.Value,
                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String,
                    }).ToList(),

                    // Variants
                    Variants = p.VariantMasters.Select(v => new CatalogVariantResponseDto
                    {
                        Id = v.Id.ToString(),
                        Name = v.Name,
                        Price = new PriceResponseDto
                        {
                            Amount = v.Price.Amount,
                            Currency = p.Price!.Currency,
                            Discount = p.Price.Discount,
                            Mrp = p.Price.Mrp,
                            Tax = p.Price.Tax
                        },
                        StockQuantity = v.StockQuantity,
                        Attributes = v.Attributes.AsEnumerable()
                                .GroupBy(a => a.AttributeGroupNameSnapshot)
                                .Select(g => new AttributeGroupResponseDto
                                {
                                    AttributeGroupName = g.Key,
                                    Attributes = g.Select(a => new AttributeResponseDto
                                    {
                                        Key = a.AttributeKey,
                                        Label = a.AttributeLabel ?? "",
                                        Value = a.Value,
                                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                                    }).ToList()
                                })
                                .ToList(),
                        Addons = v.ProductAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            },
                        }).ToList(),
                        Media = v.VariantImages.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return productDto;
        }

        public async Task<List<CatalogResponseDto>> GetFilteredProductsAsync(List<int> attributeIds,int? subCategoryId = null, int topN = 10)
        {
            var query = _productRepository.Query();

            if (subCategoryId.HasValue && subCategoryId.Value > 0)
            {
                query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
            }

            if (attributeIds != null && attributeIds.Any())
            {
                // Ensure product has all selected attribute IDs
                query = query.Where(v => v.VariantMasters.Any(vm => attributeIds.All(atr => vm.Attributes.Any(attr => attr.CatalogAttributeValueId == atr))));

            }

            // Take top N products
            var products = await query
                .Take(topN)
                .Select(p => new CatalogResponseDto
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ThumbnailUrl = p.ThumbnailUrl,
                    Rating = p.Rating,
                    Reviews = p.Reviews,
                    SubCategoryId = p.SubCategoryId.ToString(),
                    IsTrending = p.IsTrending,
                    IsFeatured = p.IsFeatured,
                    Price = new PriceResponseDto
                    {
                        Amount = p.Price.Amount,
                        Currency = p.Price.Currency,
                        Discount = p.Price.Discount,
                        Mrp = p.Price.Mrp,
                        Tax = p.Price.Tax
                    },

                    // Media
                    Media = p.ProductImages.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Addons
                    Addons = p.ProductAddons.Select(a => new AddonResponseDto
                    {
                        Name = a.Name,
                        Description = a.Description,
                        Price = new PriceResponseDto
                        {
                            Amount = a.Price.Amount,
                            Mrp = a.Price.Mrp
                        }
                    }).ToList(),

                    // Attributes
                    Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                    {
                        Key = a.AttributeKey,
                        Label = a.AttributeLabel ?? "",
                        Value = a.Value,
                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                    }).ToList(),

                    // Variants
                    Variants = p.VariantMasters.Select(v => new CatalogVariantResponseDto
                    {
                        Id = v.Id.ToString(),
                        Name = v.Name,
                        Price = new PriceResponseDto
                        {
                            Amount = v.Price.Amount,
                            Currency = v.Price.Currency,
                            Discount = v.Price.Discount,
                            Mrp = v.Price.Mrp,
                            Tax = v.Price.Tax
                        },
                        StockQuantity = v.StockQuantity,
                        Attributes = v.Attributes
                            .GroupBy(a => a.AttributeGroupNameSnapshot)
                            .Select(g => new AttributeGroupResponseDto
                            {
                                AttributeGroupName = g.Key,
                                Attributes = g.Select(a => new AttributeResponseDto
                                {
                                    Key = a.AttributeKey,
                                    Label = a.AttributeLabel ?? "",
                                    Value = a.Value,
                                    DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                                }).ToList()
                            }).ToList(),
                        Addons = v.ProductAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            }
                        }).ToList(),
                        Media = v.VariantImages.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return products;
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

        public async Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = _productRepository.Query();

                var totalCount = await queryable.CountAsync();

                var skip = (pageNumber - 1) * pageSize;                

                // Take top N products
                var products = await queryable
                    .Where(p => query.Contains(p.Name))
                    .Skip(skip)
                    .Take(pageSize)
                    .Select(p => new CatalogResponseDto
                    {
                        Id = p.Id.ToString(),
                        Name = p.Name,
                        ThumbnailUrl = p.ThumbnailUrl,
                        Rating = p.Rating,
                        Reviews = p.Reviews,
                        SubCategoryId = p.SubCategoryId.ToString(),
                        IsTrending = p.IsTrending,
                        IsFeatured = p.IsFeatured,
                        Price = new PriceResponseDto
                        {
                            Amount = p.Price.Amount,
                            Currency = p.Price.Currency,
                            Discount = p.Price.Discount,
                            Mrp = p.Price.Mrp,
                            Tax = p.Price.Tax
                        },

                        // Media
                        Media = p.ProductImages.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // Addons
                        Addons = p.ProductAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            }
                        }).ToList(),

                        // Attributes
                        Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                        {
                            Key = a.AttributeKey,
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                        }).ToList(),

                        // Variants
                        Variants = p.VariantMasters.Select(v => new CatalogVariantResponseDto
                        {
                            Id = v.Id.ToString(),
                            Name = v.Name,
                            Price = new PriceResponseDto
                            {
                                Amount = v.Price.Amount,
                                Currency = v.Price.Currency,
                                Discount = v.Price.Discount,
                                Mrp = v.Price.Mrp,
                                Tax = v.Price.Tax
                            },
                            StockQuantity = v.StockQuantity,
                            Attributes = v.Attributes
                                .GroupBy(a => a.AttributeGroupNameSnapshot)
                                .Select(g => new AttributeGroupResponseDto
                                {
                                    AttributeGroupName = g.Key,
                                    Attributes = g.Select(a => new AttributeResponseDto
                                    {
                                        Key = a.AttributeKey,
                                        Label = a.AttributeLabel ?? "",
                                        Value = a.Value,
                                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                                    }).ToList()
                                }).ToList(),
                            Addons = v.ProductAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Price = new PriceResponseDto
                                {
                                    Amount = a.Price.Amount,
                                    Mrp = a.Price.Mrp
                                }
                            }).ToList(),
                            Media = v.VariantImages.Select(img => new MediaResponseDto
                            {
                                Url = img.ImageUrl,
                                Type = img.MediaType.ToString(),
                                AltText = img.AltText,
                                SortOrder = img.SortOrder
                            }).ToList()
                        }).ToList()
                    })
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