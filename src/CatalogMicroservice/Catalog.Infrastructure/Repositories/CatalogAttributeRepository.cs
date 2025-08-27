using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogAttributeRepository : Repository<CatalogAttribute>, ICatalogAttributeRepository
    {
        private readonly CatalogDbContext dbContext;

        public CatalogAttributeRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CatalogAttribute>> GetAttributesByCategoryOrSubCategoryAsync(int categoryId, int subCategoryId, bool summaryOnly = false)
        {
            var sql = @"
                SELECT ca.*
                FROM catalog_attribute ca
                WHERE (ca.category_id = {0} OR ca.sub_category_id = {1})";

            if (!summaryOnly)
            {
                sql += " AND ca.is_summary = false";
            }

            sql += " ORDER BY ca.sort_order";

            var attributes = await dbContext.CatalogAttributes
                .FromSqlRaw(sql, categoryId, subCategoryId)
                .Include(a => a.AllowedValues)
                .Include(a => a.AttributeIcon)
                .Include(a => a.AttributeDataType)
                .Include(a => a.AttributeGroup)
                .AsNoTracking()
                .ToListAsync();

            return attributes;
        }


    }
}