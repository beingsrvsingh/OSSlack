using Microsoft.EntityFrameworkCore;
using Pooja.Application.Services;
using Pooja.Domain.Core.Repository;
using Pooja.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;


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

        public async Task<CatalogResponseDto?> GetPoojaByIdAsync(int id)
        {
            _logger.LogInfo($"Getting astrologer by Id: {id}");
            try
            {
                var query = _repository.Query();

                var pooja = await query
                    .Where(p => p.Id == id)
                    .Select(p => new CatalogResponseDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ThumbnailUrl = p.ThumbnailUrl,
                        IsActive = p.IsActive,
                        Rating = p.Rating,
                        Reviews = p.Reviews,
                        CategoryId = p.CategoryId,
                        SubCategoryId = p.SubCategoryId,
                        CategoryName = p.CategoryNameSnapshot,
                        SubCategoryName = p.SubCategoryNameSnapshot,
                        Currency = p.Currency ?? "INR",
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

                        // addons
                        Addons = p.PoojaAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Price = a.Price,
                            Description = a.Description,
                            Currency = a.Currency ?? "0"
                        }).ToList(),

                        // attributes
                        Attributes = p.PoojaAttributeValues.Select(a => new AttributeResponseDto
                        {
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId,
                        }).ToList(),

                        // Variants
                        Variants = p.PoojaVariantMasters.Select(v => new CatalogVariantResponseDto
                        {
                            Id = v.Id,
                            Name = v.Name,
                            Price = v.Price,
                            MRP = v.MRP,
                            StockQuantity = v.StockQuantity,
                            DurationMinutes = v.DurationMinutes,
                            Attributes = v.PoojaAttributeValues.Select(a => new AttributeResponseDto
                            {
                                Label = a.AttributeLabel ?? "",
                                Value = a.Value,
                                DataTypeId = a.AttributeDataTypeId,
                            }).ToList(),
                            Addons = v.PoojaAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Price = a.Price,
                                Description = a.Description,
                                Currency = a.Currency ?? "0"
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

                if (pooja == null)
                    _logger.LogWarning($"Astrologer with Id {id} not found.");
                return pooja;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving astrologer with Id {id}", ex);
                throw;
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
    }
}