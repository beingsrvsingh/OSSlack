using Kathavachak.Application.Services;
using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;
using Shared.Utilities.Response;

namespace Kathavachak.Infrastructure.Services
{
    public class KathavachakService : IKathavachakService
    {
        private readonly IKathavachakRepository _repository;
        private readonly ILoggerService<KathavachakService> _logger;

        public KathavachakService(IKathavachakRepository repository, ILoggerService<KathavachakService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int topN = 5)
        {
            List<KathavachakMaster> lstProducts = new List<KathavachakMaster>();

            lstProducts = (List<KathavachakMaster>)await _repository.GetAsync((p) => p.CategoryId == subCategoryId && p.IsTrending == true);

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

        public async Task<List<CatalogResponseDto>?> GetKathavachaksBySubCategoryIdAsync(int? subCategoryId = null, int topN = 5)
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
                        Media = p.KathavachakMedia.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // addons
                        Addons = p.KathavachakAddons.Select(a => new AddonResponseDto
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
                        Variants = p.KathavachakExpertises.Select(v => new CatalogVariantResponseDto
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
                            Attributes = v.KathavachakAttributeValues.AsEnumerable()
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
                            Addons = v.KathavachakAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Price = new PriceResponseDto
                                {
                                    Amount = a.Price.Amount,
                                    Mrp = a.Price.Mrp
                                },
                            }).ToList(),
                            Media = v.KathavachakExpertiseMedia.Select(img => new MediaResponseDto
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

        public async Task<List<CatalogResponseDto>> GetFilteredKathavachaksAsync(List<int> attributeIds, int? subCategoryId = null, int topN = 10)
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
                    p.AttributeValues.Any(av => av.CatalogAttributeValueId == attrId)));
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
                    Media = p.KathavachakMedia.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Addons
                    Addons = p.KathavachakAddons.Select(a => new AddonResponseDto
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
                    Variants = p.KathavachakExpertises.Select(v => new CatalogVariantResponseDto
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
                        Attributes = v.KathavachakAttributeValues
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
                        Addons = v.KathavachakAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            }
                        }).ToList(),
                        Media = v.KathavachakExpertiseMedia.Select(img => new MediaResponseDto
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

        public async Task<CatalogResponseDto?> GetByIdAsync(int id)
        {
            _logger.LogInfo($"Getting kathavachak by Id: {id}");
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
                    Media = p.KathavachakMedia.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Product-level addons
                    Addons = p.KathavachakAddons.Select(a => new AddonResponseDto
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
                    Variants = p.KathavachakExpertises.Select(v => new CatalogVariantResponseDto
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
                        Attributes = v.KathavachakAttributeValues.AsEnumerable()
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
                        Addons = v.KathavachakAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            },
                        }).ToList(),
                        Media = v.KathavachakExpertiseMedia.Select(img => new MediaResponseDto
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

        public async Task<IEnumerable<KathavachakMaster>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllAsync: {ex.Message}", ex);
                return Enumerable.Empty<KathavachakMaster>();
            }
        }

        public async Task<bool> CreateAsync(KathavachakMaster entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(KathavachakMaster entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
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
                var kathavachak = await _repository.GetByIdAsync(id);
                if(kathavachak == null)
                {
                    _logger.LogWarning($"Kathavachak with ID {id} not found for deletion.");
                    return false;
                }
                kathavachak.IsActive = false;
                await _repository.UpdateAsync(kathavachak);
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
                    .Where(p => EF.Functions.Like(p.Name, $"%{query}%"))
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
                        Media = p.KathavachakMedia.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // Addons
                        Addons = p.KathavachakAddons.Select(a => new AddonResponseDto
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
                        Variants = p.KathavachakExpertises.Select(v => new CatalogVariantResponseDto
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
                            Attributes = v.KathavachakAttributeValues
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
                            Addons = v.KathavachakAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Price = new PriceResponseDto
                                {
                                    Amount = a.Price.Amount,
                                    Mrp = a.Price.Mrp
                                }
                            }).ToList(),
                            Media = v.KathavachakExpertiseMedia.Select(img => new MediaResponseDto
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
