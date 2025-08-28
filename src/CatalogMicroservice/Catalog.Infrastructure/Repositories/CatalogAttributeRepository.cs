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

        public async Task<List<CatalogAttributeRaw>> GetFilterableAttributes(int categoryId, int subCategoryId)
        {
            var categoryMasterIdParam = new MySqlParameter("@CategoryMasterId", (object)categoryId);
            var subCategoryMasterIdParam = new MySqlParameter("@SubCategoryMasterId", (object)subCategoryId);

            var rawResults = await dbContext.RawAttributeValues
                            .FromSqlRaw(@"
                                SELECT
                                    attr.id AS Id,
                                    attr.key AS `Key`,
                                    attr.category_id AS CategoryMasterId,
                                    attr.sub_category_id AS SubCategoryMasterId,
                                    attr.label AS Label,
                                    av.value AS AllowedValue,
                                    av.sort_order AS AllowedValueSortOrder,
                                    attr.sort_order AS AttributeSortOrder 
                                FROM catalog_attribute attr
                                INNER JOIN attribute_allowed_value av ON av.attribute_id = attr.id
                                WHERE 
                                    (@CategoryMasterId IS NOT NULL AND attr.category_id = @CategoryMasterId)
                                    OR
                                    (@SubCategoryMasterId IS NOT NULL AND attr.sub_category_id = @SubCategoryMasterId)
                                ORDER BY attr.sort_order, av.sort_order",
                                categoryMasterIdParam, subCategoryMasterIdParam)
                            .AsNoTracking()
                            .ToListAsync();

            return rawResults;
        }

    }
}