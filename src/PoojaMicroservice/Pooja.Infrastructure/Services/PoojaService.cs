using Microsoft.EntityFrameworkCore;
using Pooja.Application.Services;
using Pooja.Domain.Core.Repository;
using Pooja.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;
using Shared.Utilities.Response;


namespace Pooja.Infrastructure.Services
{
    public class PoojaService : IPoojaService
    {
        private readonly IPoojaMasterRepository _repository;
        private readonly ILoggerService<PoojaService> _logger;

        public PoojaService(IPoojaMasterRepository repository, ILoggerService<PoojaService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<PoojaMaster>> GetAllPoojasAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve poojas.");
                return Enumerable.Empty<PoojaMaster>();
            }
        }

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int topN = 5)
        {
            List<PoojaMaster> lstProducts = new List<PoojaMaster>();

            lstProducts = (List<PoojaMaster>)await _repository.GetAsync((p) => p.CategoryId == subCategoryId && p.IsTrending == true);

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

        public async Task<List<CatalogResponseDto>?> GetPoojasBySubCategoryIdAsync(int? subCategoryId = null, int topN = 5)
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
                        Media = p.PoojaImages.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // addons
                        Addons = p.PoojaAddons.Select(a => new AddonResponseDto
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
                        Attributes = p.PoojaAttribute.Select(a => new AttributeResponseDto
                        {
                            Key = a.AttributeKey,
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String,
                        }).ToList(),

                        // Variants
                        Variants = p.PoojaVariantMasters.Select(v => new CatalogVariantResponseDto
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
                            Attributes = v.PoojaAttributeValues.AsEnumerable()
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
                            Addons = v.PoojaAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Price = new PriceResponseDto
                                {
                                    Amount = a.Price.Amount,
                                    Mrp = a.Price.Mrp
                                },
                            }).ToList(),
                            Media = v.PoojaVariantImages.Select(img => new MediaResponseDto
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

        public async Task<List<CatalogResponseDto>> GetFilteredPoojasAsync(List<int> attributeIds, int? subCategoryId = null, int topN = 10)
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
                    p.PoojaAttribute.Any(av => av.CatalogAttributeValueId == attrId)));
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
                    Media = p.PoojaImages.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Addons
                    Addons = p.PoojaAddons.Select(a => new AddonResponseDto
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
                    Attributes = p.PoojaAttribute.Select(a => new AttributeResponseDto
                    {
                        Key = a.AttributeKey,
                        Label = a.AttributeLabel ?? "",
                        Value = a.Value,
                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                    }).ToList(),

                    // Variants
                    Variants = p.PoojaVariantMasters.Select(v => new CatalogVariantResponseDto
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
                        Attributes = v.PoojaAttributeValues
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
                        Addons = v.PoojaAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            }
                        }).ToList(),
                        Media = v.PoojaVariantImages.Select(img => new MediaResponseDto
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

        public async Task<CatalogResponseDto?> GetPoojaByIdAsync(int id)
        {
            _logger.LogInfo($"Getting astrologer by Id: {id}");
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
                    Media = p.PoojaImages.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList(),

                    // Product-level addons
                    Addons = p.PoojaAddons.Select(a => new AddonResponseDto
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
                    Attributes = p.PoojaAttribute.Select(a => new AttributeResponseDto
                    {
                        Key = a.AttributeKey,
                        Label = a.AttributeLabel ?? "",
                        Value = a.Value,
                        DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String,
                    }).ToList(),

                    // Variants
                    Variants = p.PoojaVariantMasters.Select(v => new CatalogVariantResponseDto
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
                        Attributes = v.PoojaAttributeValues.AsEnumerable()
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
                        Addons = v.PoojaAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Description = a.Description,
                            Price = new PriceResponseDto
                            {
                                Amount = a.Price.Amount,
                                Mrp = a.Price.Mrp
                            },
                        }).ToList(),
                        Media = v.PoojaVariantImages.Select(img => new MediaResponseDto
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

        public async Task<IEnumerable<PoojaMaster>> GetPoojasByTempleAsync(int templeId)
        {
            try
            {
                return await _repository.GetByTempleAsync(templeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve poojas for temple id: {templeId}");
                return Enumerable.Empty<PoojaMaster>();
            }
        }

        public async Task<IEnumerable<PoojaMaster>> GetPoojasByPriestAsync(int priestId)
        {
            try
            {
                return await _repository.GetByPriestAsync(priestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve poojas for priest id: {priestId}");
                return Enumerable.Empty<PoojaMaster>();
            }
        }

        public async Task AddPoojaAsync(PoojaMaster pooja)
        {
            try
            {
                await _repository.AddAsync(pooja);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add pooja.");
                throw;
            }
        }

        public async Task UpdatePoojaAsync(PoojaMaster pooja)
        {
            try
            {
                await _repository.UpdateAsync(pooja);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update pooja with id: {pooja.Id}");
                throw;
            }
        }

        public async Task DeletePoojaAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete pooja with id: {id}");
                throw;
            }
        }

        public async Task<IEnumerable<PoojaMaster>> SearchPoojasAsync(string keyword)
        {
            try
            {
                var all = await _repository.GetAllAsync();
                return all.Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to search poojas with keyword: {keyword}");
                return Enumerable.Empty<PoojaMaster>();
            }
        }

        public async Task<bool> IsPoojaAvailableAtHomeAsync(int poojaId)
        {
            try
            {
                var pooja = await _repository.GetByIdAsync(poojaId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to check if pooja id {poojaId} is available at home.");
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
                        Media = p.PoojaImages.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // Addons
                        Addons = p.PoojaAddons.Select(a => new AddonResponseDto
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
                        Attributes = p.PoojaAttribute.Select(a => new AttributeResponseDto
                        {
                            Key = a.AttributeKey,
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId ?? (int)AttributeDataType.String
                        }).ToList(),

                        // Variants
                        Variants = p.PoojaVariantMasters.Select(v => new CatalogVariantResponseDto
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
                            Attributes = v.PoojaAttributeValues
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
                            Addons = v.PoojaAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Price = new PriceResponseDto
                                {
                                    Amount = a.Price.Amount,
                                    Mrp = a.Price.Mrp
                                }
                            }).ToList(),
                            Media = v.PoojaVariantImages.Select(img => new MediaResponseDto
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