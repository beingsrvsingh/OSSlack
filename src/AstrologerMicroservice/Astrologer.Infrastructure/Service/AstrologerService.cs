using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Application.Service;
using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;

namespace AstrologerMicroservice.Infrastructure.Service
{

    public class AstrologerService : IAstrologerService
    {
        private readonly IAstrologerRepository _repository;
        private readonly ILoggerService<AstrologerService> _logger;

        public AstrologerService(IAstrologerRepository repository, ILoggerService<AstrologerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<AstrologerMaster>> GetAvailableAsync(DateTime date, string language, string expertise)
        {
            try
            {
                return await _repository.GetAvailableAsync(date, language, expertise);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch available astrologers.");
                return Enumerable.Empty<AstrologerMaster>();
            }
        }

        public async Task<CatalogResponseDto?> GetByIdAsync(int astrologerId)
        {
            _logger.LogInfo($"Getting astrologer by Id: {astrologerId}");
            try
            {
                var query = _repository.Query();

                var astrologer = await query
                    .Where(p => p.Id == astrologerId)
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
                        Media = p.AstrologerMedia.Select(img => new MediaResponseDto
                        {
                            Url = img.ImageUrl,
                            Type = img.MediaType.ToString(),
                            AltText = img.AltText,
                            SortOrder = img.SortOrder
                        }).ToList(),

                        // Astrologer-level addons
                        Addons = p.AstrologerAddons.Select(a => new AddonResponseDto
                        {
                            Name = a.Name,
                            Price = a.Price,
                            Description = a.Description,
                            Currency = a.Currency ?? "0"
                        }).ToList(),

                        // Astrologer-level attributes
                        Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                        {
                            Label = a.AttributeLabel ?? "",
                            Value = a.Value,
                            DataTypeId = a.AttributeDataTypeId,
                        }).ToList(),

                        // Variants
                        Variants = p.AstrologerExpertises.Select(v => new CatalogVariantResponseDto
                        {
                            Id = v.Id,
                            Name = v.Name,
                            Price = v.Price,
                            MRP = v.MRP,
                            StockQuantity = v.StockQuantity,
                            DurationMinutes = v.DurationMinutes,
                            Attributes = v.AstrologerAttributeValues.Select(a => new AttributeResponseDto
                            {
                                Label = a.AttributeLabel ?? "",
                                Value = a.Value,
                                DataTypeId = a.AttributeDataTypeId,
                            }).ToList(),
                            Addons = v.AstrologerAddons.Select(a => new AddonResponseDto
                            {
                                Name = a.Name,
                                Price = a.Price,
                                Description = a.Description,
                                Currency = a.Currency ?? "0"
                            }).ToList(),
                            Media = v.AstrologerExpertiseMedia.Select(img => new MediaResponseDto
                            {
                                Url = img.ImageUrl,
                                Type = img.MediaType.ToString(),
                                AltText = img.AltText,
                                SortOrder = img.SortOrder
                            }).ToList()
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (astrologer == null)
                    _logger.LogWarning($"Astrologer with Id {astrologerId} not found.");
                return astrologer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving astrologer with Id {astrologerId}", ex);
                throw;
            }
        }

        public async Task<AstrologerMaster?> GetByUserIdAsync(string userId)
        {
            _logger.LogInfo($"Getting astrologer by UserId: {userId}");
            try
            {
                var astrologer = await _repository.GetByIdAsync(userId);
                if (astrologer == null)
                    _logger.LogWarning($"Astrologer with UserId {userId} not found.");
                return astrologer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving astrologer with UserId {userId}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<AstrologerMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            _logger.LogInfo($"Getting all astrologers - Page: {page}, PageSize: {pageSize}");
            try
            {
                return await _repository.GetAllAsync(page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving astrologers", ex);
                throw;
            }
        }

        public async Task<bool> CreateAsync(CreateAstrologerCommand command)
        {
            _logger.LogInfo("Creating new astrologer");

            try
            {
                // Check if astrologer exists by unique field, e.g. Email
                var exists = await _repository.AnyAsync(a => a.Id.ToString() == command.UserId);
                if (exists)
                {
                    _logger.LogWarning($"Astrologer with userId {command.UserId} already exists.");
                    return false; // or you can throw, or handle as needed
                }

                // Create new astrologer
                var astrologer = command.Adapt<AstrologerMaster>();

                astrologer.AstrologerLanguages.Clear();
                foreach (var languageEnum in command.Languages)
                {
                    astrologer.AstrologerLanguages.Add(new AstrologerLanguage
                    {
                        Id = (int)languageEnum
                    });
                }

                astrologer.AstrologerExpertises.Clear();
                //foreach (var expertise in command.Expertise)
                //{
                //    astrologer.AstrologerExpertises.Add(new AstrologerExpertise
                //    {
                //        ExpertiseId = (int)expertise
                //    });
                //}

                await _repository.AddAsync(astrologer);
                await _repository.SaveChangesAsync();

                _logger.LogInfo($"Astrologer with userId {command.UserId} created successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating astrologer", ex);
                return false;
            }
        }


        public async Task<bool> UpdateAsync(UpdateAstrologerCommand command)
        {
            _logger.LogInfo($"Updating astrologer with Id {command.Id}");

            try
            {
                var astrologer = await _repository.GetAstrologerWithLanguagesAndExpertisesAsync(command.Id);
                if (astrologer == null)
                {
                    _logger.LogWarning($"Astrologer with Id {command.Id} not found.");
                    return false;
                }

                // Update scalar properties
                astrologer.Adapt(command);

                // Clear existing languages and insert new ones
                astrologer.AstrologerLanguages.Clear();
                foreach (var langEnum in command.Languages)
                {
                    astrologer.AstrologerLanguages.Add(new AstrologerLanguage
                    {
                        Id = (int)langEnum
                    });
                }

                // Clear existing expertises and insert new ones
                astrologer.AstrologerExpertises.Clear();
                //foreach (var expEnum in command.Expertise)
                //{
                //    astrologer.AstrologerExpertises.Add(new AstrologerExpertise
                //    {
                //        ExpertiseId = (int)expEnum
                //    });
                //}

                await _repository.UpdateAsync(astrologer);
                await _repository.SaveChangesAsync();

                _logger.LogInfo($"Astrologer with Id {command.Id} updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating astrologer with Id {command.Id}: {ex.Message}", ex);
                return false;
            }
        }



        public async Task<bool> DeleteAsync(int astrologerId)
        {
            _logger.LogInfo($"Deleting astrologer with Id: {astrologerId}");
            try
            {
                var astrologer = await _repository.GetAstrologerWithLanguagesAndExpertisesAsync(astrologerId);
                if (astrologer is null)
                {
                    _logger.LogInfo($"astrologer not found with Id: {astrologerId}");
                    return false;   
                }
                await _repository.DeleteAsync(astrologer);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting astrologer with Id {astrologerId}", ex);
                throw;
            }
        }

        public async Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var (products, totalCount) = await _repository.SearchAsync(query, page, pageSize, cancellationToken);

                var resultDtos = products.Select(p => new SearchItemDto
                {
                    Pid = p.Id.ToString(),
                    CategoryId = p.CategoryId.ToString(),
                    SubCategoryId = p.SubcategoryId.ToString(),
                    Name = p.Name ?? "",
                    //Cost = (double)(p.Price ?? 0),
                    ThumbnailUrl = p.ThumbnailUrl ?? "",
                    //CategoryType = "Temple",
                    //Quantity = 1,
                    //Limit = 1,
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

                var filterMeta = new BaseSearchFilterMetadata
                {
                    TotalCount = totalCount,
                    HasMoreResults = page * pageSize < totalCount,
                    Score = products.FirstOrDefault()?.Score ?? 0,
                    MatchType = matchType,
                    EnableFilters = enableFilters,
                    Source = "Astrologer"
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
                        Source = "Astrologer"
                    }
                };
            }
        }
    }

}