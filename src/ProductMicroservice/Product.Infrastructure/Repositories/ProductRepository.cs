using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Product.Domain.Entities;
using Product.Domain.Repository;
using Product.Infrastructure.Persistence.Context;
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
                .Where(v => v.ProductId == productId)
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
            string booleanQuery = $"\"{normalizedQuery}\"";

            int skip = (page - 1) * pageSize;

            var sql = $@"
                        SELECT 
                            p.Id, 
                            Name, 
                            thumbnail_url AS ThumbnailUrl, 
                            Price,
                            cat_snap AS CategoryNameSnapshot, 
                            subcat_snap AS SubCategoryNameSnapshot,
                            category_id AS CategoryId,
                            subcategory_id AS SubcategoryId,

                            pe.catalog_attribute_id as CatalogAttributeId,
                            pe.cat_attr_val_id as CatalogAttributeValueId,
                            pe.value as CatalogAttributeValue,
                            pe.attribute_key as CatalogAttributeKey,
                            pe.attribute_label as CatalogAttributeLabel,
                            pe.attribute_datatype_id as CatalogAttributeDatatype,

                            LEAST(ROUND(IFNULL(MATCH(name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS NameScore,
                            LEAST(ROUND(IFNULL(MATCH(cat_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS CatScore,
                            LEAST(ROUND(IFNULL(MATCH(subcat_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS SubcatScore,
                            (
                                LEAST(ROUND(IFNULL(MATCH(name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 3 +
                                LEAST(ROUND(IFNULL(MATCH(cat_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 2 +
                                LEAST(ROUND(IFNULL(MATCH(subcat_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 1
                            ) AS TotalScore

                        FROM product_master p
                        LEFT JOIN product_attribute_value pe ON p.id = pe.product_id
                        WHERE 
                            MATCH(name) AGAINST ({{0}} IN BOOLEAN MODE)
                            OR MATCH(cat_snap) AGAINST ({{0}} IN BOOLEAN MODE)
                            OR MATCH(subcat_snap) AGAINST ({{0}} IN BOOLEAN MODE)
                        ORDER BY TotalScore DESC
                        LIMIT {{1}} OFFSET {{2}};";

            var products = await _context.ProductSearchRaws
                .FromSqlRaw(sql, booleanQuery, pageSize, skip)
                .ToListAsync(cancellationToken);

            var groupedResults = products
                                .GroupBy(p => p.Id)
                                .Select(g =>
                                {
                                    var product = g.First();

                                    product.AttributeValues = g
                                        .Where(x => x.CatalogAttributeId.HasValue)
                                        .Select(x => new BaseAttributeValue
                                        {
                                            CatalogAttributeId = x.CatalogAttributeId ?? 0,
                                            CatalogAttributeValueId = x.CatalogAttributeValueId ?? 0,
                                            Value = x.CatalogAttributeValue,
                                            AttributeKey = x.CatalogAttributeKey,
                                            AttributeLabel = x.CatalogAttributeLabel,
                                            AttributeDataTypeId = x.CatalogAttributeDatatype ?? 0
                                        })
                                        .ToList();

                                    return product;
                                })
                                .ToList();

            var countSql = @"
                            SELECT COUNT(*) FROM product_master p
                            LEFT JOIN product_attribute_value pe ON p.id = pe.product_id
                            WHERE 
                            MATCH(name) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(cat_snap) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(subcat_snap) AGAINST ({0} IN BOOLEAN MODE)";

            var totalCount = await _context.ProductMasters
                .FromSqlRaw(countSql, booleanQuery)
                .CountAsync(cancellationToken);

            return (groupedResults, totalCount);
        }
    }

}