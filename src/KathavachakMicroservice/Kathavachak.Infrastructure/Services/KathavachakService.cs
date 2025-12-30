using Kathavachak.Application.Services;
using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Catalog.Queries;
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

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10)
        {
            List<KathavachakMaster> lstProducts = new List<KathavachakMaster>();

            lstProducts = (List<KathavachakMaster>)await _repository.GetAsync((p) => p.CategoryId == subCategoryId && p.IsTrending == true);

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
                var queryable = _repository.Query();

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

        public async Task<List<CatalogResponseDto>?> GetKathavachaksBySubCategoryIdAsync(int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
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

        public async Task<List<CatalogResponseDto>> GetFilteredKathavachaksAsync(List<int> attributeIds, int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
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
                    p.Attributes.Any(av => av.CatalogAttributeValueId == attrId)));
            }

            var products = await query
                .Skip(pageNumber)
                .Take(pageSize)
                .Select(CatalogQueries.ToCatalogResponse)                
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

                queryable = CatalogQueries.ApplySearch(queryable, query);

                var totalCount = await queryable.CountAsync();

                var skip = (pageNumber - 1) * pageSize;

                var products = await queryable
                    .Where(p => EF.Functions.Like(p.Name, $"%{query}%"))
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
