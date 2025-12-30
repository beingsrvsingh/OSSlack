using Microsoft.EntityFrameworkCore;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;
using Shared.Infrastructure.Repositories;
using Shared.Utilities.Response;
using System.Linq;
using Temple.Application.Services;
using Temple.Domain.Entities;
using Temple.Domain.Repositories;
using Temple.Infrastructure.Persistence.Catalog.Queries;

namespace Temple.Infrastructure.Services
{
    public class TempleService : ITempleService
    {
        private readonly ITempleRepository _repository;
        private readonly ILoggerService<TempleService> _logger;

        public TempleService(ITempleRepository repository, ILoggerService<TempleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _repository.GetAllAsync(page, pageSize);
        }

        public async Task<List<TrendingResponse>> GetSubcategoryTrendingAsync(int? subCategoryId, int pageNumber = 1, int pageSize = 10)
        {
            List<TempleMaster> lstProducts = new List<TempleMaster>();

            lstProducts = (List<TempleMaster>)await _repository.GetAsync((p) => p.CategoryId == subCategoryId && p.IsTrending == true);

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

        public async Task<List<CatalogResponseDto>?> GetTemplesBySubCategoryIdAsync(int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
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

        public async Task<CatalogResponseDto?> GetTempleByIdWithDetailsAsync(int id)
        {
            try
            {
                var queryable = _repository.Query();

                var productDto = await queryable
                                    .AsNoTracking()
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

        public async Task<List<CatalogResponseDto>> GetFilteredTemplesAsync(List<int> attributeIds, int? subCategoryId = null, int pageNumber = 1, int pageSize = 10)
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

            var products = await queryable
                                    .AsNoTracking()
                                    .Skip(pageNumber)
                                    .Take(pageSize)
                                    .Select(CatalogQueries.ToCatalogResponse)
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
