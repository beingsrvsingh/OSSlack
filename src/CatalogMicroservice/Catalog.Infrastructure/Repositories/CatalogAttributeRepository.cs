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
                .AsNoTracking()
                .ToListAsync();

            return attributes;
        }

        public async Task<List<CatalogAttributeRaw>> GetFilterableAttributes(int Scid)
        {
            var subCategoryIdParam = new MySqlParameter("@SubCategoryId", (object)Scid);

            var rawResults = await dbContext.RawAttributeValues
                .FromSqlRaw(@"
                            SELECT
                                attr.id AS Id,
                                attr.catalog_attribute_key AS `Key`,
                                cam.category_id AS CategoryMasterId,
                                cam.sub_category_id AS SubCategoryMasterId,
                                attr.label AS Label,
                                av.id AS AllowedValueId,
                                av.value AS AllowedValue,
                                av.sort_order AS AllowedValueSortOrder,
                                attr.sort_order AS AttributeSortOrder
                            FROM category_attribute_map cam
                            INNER JOIN catalog_attribute attr 
                                ON cam.attribute_id = attr.id
                            INNER JOIN subcategory_allowed_value sav
                                ON sav.attribute_id = attr.id
                               AND sav.sub_category_id = cam.sub_category_id
                            INNER JOIN attribute_allowed_value av
                                ON av.id = sav.allowed_value_id
                            WHERE cam.sub_category_id = @SubCategoryId
                            ORDER BY attr.sort_order, av.sort_order",
                                    subCategoryIdParam)
                                .AsNoTracking()
                                .ToListAsync();

            return rawResults;
        }


    }
}