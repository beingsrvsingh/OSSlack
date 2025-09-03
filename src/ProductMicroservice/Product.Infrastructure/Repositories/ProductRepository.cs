using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Product.Domain.Entities;
using Product.Domain.Repository;
using Product.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

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

        public async Task<List<ProductMaster>> GetProductsByIdAndCategoryIdAsync(List<int> pids, int cid)
        {
            return await _context.ProductMasters.Where(p => pids.Contains(p.Id) && p.CategoryId == cid).ToListAsync();
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
                                p.price AS Price, 
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

    }

}