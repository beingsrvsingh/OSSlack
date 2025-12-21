using Microsoft.EntityFrameworkCore;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;
using Shared.Utilities.Response;
using Temple.Application.Services;
using Temple.Domain.Entities;
using Temple.Domain.Repositories;

namespace Temple.Infrastructure.Services
{
    public class TempleService : ITempleService
    {
        private readonly ITempleRepository _repository;
        private readonly ILoggerService<TempleService> _logger;

        public TempleService(ITempleRepository templeRepository, ILoggerService<TempleService> logger)
        {
            _repository = templeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _repository.GetAllAsync(page, pageSize);
        }

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int topN = 5)
        {
            List<TempleMaster> lstProducts = new List<TempleMaster>();

            lstProducts = (List<TempleMaster>)await _repository.GetAsync((p) => p.CategoryId == subCategoryId && p.IsTrending == true);

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

        public async Task<List<CatalogResponseDto>?> GetTemplesBySubCategoryIdAsync(int? subCategoryId = null, int topN = 5)
        {
            try
            {
                // Use IQueryable from repository
                var query = _repository.Query();

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
                        Media = p.TempleImages.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // addons
                        Addons = p.TempleAddons.Select(a => new AddonResponseDto
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
                        Attributes = p.TempleAttributes.Select(a => new AttributeResponseDto
                        {
                            Key = a.AttributeKey,
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String,
                        }).ToList(),

                        // Variants
                        Variants = p.TempleExpertises.Select(v => new CatalogVariantResponseDto
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
                            Attributes = v.AttributeValues.AsEnumerable()
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
                            Addons = v.TempleAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Price = new PriceResponseDto
                                {
                                    Amount = a.Price.Amount,
                                    Mrp = a.Price.Mrp
                                },
                            }).ToList(),
                            Media = v.TempleExpertiseImages.Select(img => new MediaResponseDto
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

        public async Task<CatalogResponseDto?> GetTempleByIdWithDetailsAsync(int id)
        {
            try
            {
                var query = _repository.Query();

                var productDto = await query
                .Where(p => p.Id == id)
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
                    Media = p.TempleImages.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Product-level addons
                    Addons = p.TempleAddons.Select(a => new AddonResponseDto
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
                    Attributes = p.TempleAttributes.Select(a => new AttributeResponseDto
                    {
                        Key = a.AttributeKey,
                        Label = a.AttributeLabel ?? "",
                        Value = a.Value,
                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String,
                    }).ToList(),

                    // Variants
                    Variants = p.TempleExpertises.Select(v => new CatalogVariantResponseDto
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
                        Attributes = v.AttributeValues.AsEnumerable()
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
                        Addons = v.TempleAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            },
                        }).ToList(),
                        Media = v.TempleExpertiseImages.Select(img => new MediaResponseDto
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
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetByIdWithDetailsAsync: {ex.Message}", ex);
                return null;
            }
        }

        public async Task<List<CatalogResponseDto>> GetFilteredTemplesAsync(List<int> attributeIds, int? subCategoryId = null, int topN = 10)
        {
            var query = _repository.Query();

            if (subCategoryId.HasValue && subCategoryId.Value > 0)
            {
                query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
            }

            if (attributeIds != null && attributeIds.Any())
            {
                // Ensure product has all selected attribute IDs
                query = query.Where(p => attributeIds.All(attrId =>
                    p.TempleAttributes.Any(av => av.CatalogAttributeValueId == attrId)));
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
                    Media = p.TempleImages.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Addons
                    Addons = p.TempleAddons.Select(a => new AddonResponseDto
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
                    Attributes = p.TempleAttributes.Select(a => new AttributeResponseDto
                    {
                        Key = a.AttributeKey,
                        Label = a.AttributeLabel ?? "",
                        Value = a.Value,
                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                    }).ToList(),

                    // Variants
                    Variants = p.TempleExpertises.Select(v => new CatalogVariantResponseDto
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
                        Attributes = v.AttributeValues
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
                        Addons = v.TempleAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            }
                        }).ToList(),
                        Media = v.TempleExpertiseImages.Select(img => new MediaResponseDto
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

        public async Task<bool> CreateAsync(TempleMaster temple)
        {
            try
            {
                await _repository.AddAsync(temple);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TempleMaster temple)
        {
            try
            {
                await _repository.UpdateAsync(temple);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null) return false;

                await _repository.DeleteAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = _repository.Query();

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
                        Media = p.TempleImages.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // Addons
                        Addons = p.TempleAddons.Select(a => new AddonResponseDto
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
                        Attributes = p.TempleAttributes.Select(a => new AttributeResponseDto
                        {
                            Key = a.AttributeKey,
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                        }).ToList(),

                        // Variants
                        Variants = p.TempleExpertises.Select(v => new CatalogVariantResponseDto
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
                            Attributes = v.AttributeValues
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
                            Addons = v.TempleAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Price = new PriceResponseDto
                                {
                                    Amount = a.Price.Amount,
                                    Mrp = a.Price.Mrp
                                }
                            }).ToList(),
                            Media = v.TempleExpertiseImages.Select(img => new MediaResponseDto
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
