using Astrologer.Infrastructure.Persistence.Catalog.Queries;
using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Application.Service;
using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;
using Shared.Utilities.Response;
using System.Linq;

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

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 0, int pageSize = 10)
        {
            List<AstrologerMaster> lstProducts = new List<AstrologerMaster>();

            lstProducts = (List<AstrologerMaster>)await _repository.GetAsync((p) => (subCategoryId == null || p.CategoryId == subCategoryId) || p.IsTrending == true);

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

        public async Task<List<CatalogResponseDto>?> GetAstrologersBySubCategoryIdAsync(int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                // Use IQueryable from repository
                var queryable = _repository.Query();

                if (subCategoryId.HasValue && subCategoryId.Value > 0)
                {
                    queryable = queryable.Where(p => p.SubCategoryId == subCategoryId.Value);
                }

                var products = await queryable
                                    .AsNoTracking()
                                    .Skip(pageNumber)
                                    .Take(pageSize)
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

        public async Task<List<CatalogResponseDto>> GetFilteredAstrologersAsync(List<int> attributeIds, int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
        {
            var queryable = _repository.Query();

            if (subCategoryId.HasValue && subCategoryId.Value > 0)
            {
                queryable = queryable.Where(p => p.SubCategoryId == subCategoryId.Value);
            }

            if (attributeIds != null && attributeIds.Any())
            {
                // Ensure product has all selected attribute IDs
                queryable = queryable.Where(p => attributeIds.All(attrId =>
                    p.AttributeValues.Any(av => av.CatalogAttributeValueId == attrId)));
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

        public async Task<CatalogResponseDto?> GetByIdAsync(int astrologerId)
        {
            _logger.LogInfo($"Getting astrologer by Id: {astrologerId}");
            try
            {
                var queryable = _repository.Query();

                var productDto = await queryable
                                .AsNoTracking()
                                .Where(p => p.Id == astrologerId)
                                .Select(CatalogQueries.ToCatalogResponse)
                                .FirstOrDefaultAsync();

                return productDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetByIdWithDetailsAsync: {ex.Message}", ex);
                return null;
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

        public async Task<IEnumerable<AstrologerMaster>> GetAllAsync(int page = 1, int pageNumber = 1, int pageSize = 10)
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

                astrologer.VariantMasters.Clear();
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
                astrologer.VariantMasters.Clear();
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

        public async Task<PagedResult<CatalogResponseDto>> SearchAsync(string query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = _repository.Query();

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