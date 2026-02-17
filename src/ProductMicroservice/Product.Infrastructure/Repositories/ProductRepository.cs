using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Product.Domain.Entities;
using Product.Domain.Repository;
using Product.Infrastructure.Persistence.Catalog.Queries;
using Product.Infrastructure.Persistence.Context;
using Shared.Application.Common.Contracts.Response;
using Shared.Domain.Entities.Base;
using Shared.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : Repository<ProductMaster>, IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<decimal> GetPriceAsync(int productId)
        {
            var amount = await _context.ProductVariantMasters.Where(x => x.Id == productId).Select(x => x.Price.Amount).FirstOrDefaultAsync();

            return amount;
        }

        public async Task<ProductMaster?> GetProductWithVariantsAsync(int productId)
        {
            return await _context.ProductMasters.AsNoTracking()
                .Include(p => p.VariantMasters) // Assuming navigation property
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<ProductRegionPriceMaster>> GetRegionPricesAsync(int productId)
        {
            return await _context.ProductRegionPriceMasters.AsNoTracking()
                .Where(p => p.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductVariantMaster>> GetVariantsAsync(int productId)
        {
            return await _context.ProductVariantMasters.AsNoTracking()
                .Where(v => v.ProductMasterId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<LocalizedProductInfoMaster>> GetLocalizedInfoAsync(int productId)
        {
            return await _context.LocalizedProductInfoMasters.AsNoTracking()
                .Where(l => l.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductTagMaster>> GetTagsAsync(int productId)
        {
            return await _context.ProductTagMasters.AsNoTracking()
                .Where(t => t.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductSEOInfoMaster?> GetSEOInfoAsync(int productId)
        {
            return await _context.ProductSEOInfoMasters.AsNoTracking()
                .FirstOrDefaultAsync(s => s.ProductId == productId);
        }

        public async Task<List<ProductMaster>> GetAsync(Expression<Func<ProductMaster, bool>> predicate, Func<IQueryable<ProductMaster>, IQueryable<ProductMaster>>? include = null)
        {
            IQueryable<ProductMaster> query = _context.ProductMasters;

            if (include != null)
                query = include(query);

            return await query.AsNoTracking().Where(predicate).ToListAsync();
        }

        public IQueryable<ProductMaster> Query()
        {
            return _context.ProductMasters.AsNoTracking();
        }

        public async Task<ProductMaster?> GetSingleAsync(Expression<Func<ProductMaster, bool>> predicate, Func<IQueryable<ProductMaster>, IQueryable<ProductMaster>>? include = null)
        {
            IQueryable<ProductMaster> query = _context.ProductMasters;

            if (include != null)
                query = include(query);

            return await query.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<ProductMaster>> GetProductsByIdAndCategoryIdAsync(List<int> pids, int? cid)
        {
            var query = _context.ProductMasters.AsQueryable();

            query = query.Where(p => pids.Contains(p.Id));

            if (cid.HasValue && cid.Value != 0)
            {
                query = query.Where(p => p.CategoryId == cid.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<ProductFilterRawResult>> GetFilteredProductsRawAsync(
        List<int> attributeIds,
        int pageNumber,
        int pageSize,
        string? sortBy = null,
        bool sortDescending = false)
        {
            if (attributeIds == null || attributeIds.Count == 0)
                throw new ArgumentException("At least one attribute ID must be provided.");

            var sortColumn = string.IsNullOrWhiteSpace(sortBy) ? "p.id" : $"p.{sortBy}";
            var sortDirection = sortDescending ? "DESC" : "ASC";

            // Build SQL
            var finalSql = $@"
                            SELECT 
                                p.id AS Id, 
                                p.name AS Name, 
                                p.thumbnail_url AS ThumbnailUrl, 
                                pav.price AS Price, 
                                p.category_id AS CategoryId, 
                                p.subcategory_id AS SubCategoryId,
                                p.cat_snap AS CategoryName,
                                pav.attribute_key AS AttributeKey, 
                                pav.attribute_label AS AttributeLabel, 
                                pav.value AS Value
                            FROM product_master p
                            INNER JOIN product_attribute_value pav ON pav.product_id = p.id
                            WHERE pav.cat_attr_val_id IN ({string.Join(",", attributeIds.Select((_, i) => $"@attr{i}"))})
                            ORDER BY {sortColumn} {sortDirection}
                            LIMIT @pageSize OFFSET @offset";


            // Build parameters
            var parameters = new List<MySqlParameter>();
            for (int i = 0; i < attributeIds.Count; i++)
            {
                parameters.Add(new MySqlParameter($"@attr{i}", attributeIds[i]));
            }

            parameters.Add(new MySqlParameter("@offset", (pageNumber - 1) * pageSize));
            parameters.Add(new MySqlParameter("@pageSize", pageSize));

            return await _context.ProductFilterRawResults
                .FromSqlRaw(finalSql, parameters.ToArray())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<ProductSearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            string normalizedQuery = query.Trim();

            // Wrap query in double quotes for phrase search in BOOLEAN MODE
            string booleanQuery = $"+\"{normalizedQuery}\"";

            int skip = (page - 1) * pageSize;

            var escapedQuery = MySqlHelper.EscapeString(booleanQuery);

            var sql = @"
                        SELECT 
                            pv.Id, 
                            pv.Name, 
                            p.thumbnail_url AS ThumbnailUrl, 
                            pv.Amount AS Price,
                            p.category_id AS CategoryId,
                            p.sub_category_id AS SubcategoryId,
                            LEAST(
                                ROUND(IFNULL(MATCH(pv.name) AGAINST (@q IN NATURAL LANGUAGE MODE), 0), 3),
                                1000
                            ) AS NameScore,
                            COUNT(*) OVER() AS TotalCount
                        FROM product_master p
                        INNER JOIN product_variant_master pv 
                            ON p.id = pv.product_master_id
                        WHERE MATCH(pv.name) AGAINST (@q IN NATURAL LANGUAGE MODE)
                        LIMIT @pageSize OFFSET @skip;";

            var products = await _context.ProductSearchRaws
                            .FromSqlRaw(
                                sql,
                                new MySqlParameter("@q", escapedQuery),
                                new MySqlParameter("@pageSize", pageSize),
                                new MySqlParameter("@skip", skip)
                            ).ToListAsync(cancellationToken);

            int totalCount = products.FirstOrDefault()?.TotalCount ?? 0;

            return (products, totalCount);
        }

        public async Task<List<CatalogResponseDto>> GetCatalogAsync(string? search,int skip,int pageSize)
        {
            IQueryable<ProductMaster> query = _context.ProductMasters;

            // Apply filters
            query = CatalogQueries.ApplySearch(query, search);

            // Execute query
            return await query
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .Select(CatalogQueries.ToCatalogResponse)
                .ToListAsync();
        }
    }

}