using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities.Base;
using Shared.Infrastructure.Repositories;
using Temple.Domain.Entities;
using Temple.Domain.Repositories;
using Temple.Infrastructure.Persistence.Context;
using TempleMicroservice.Domain.Entities;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class TempleRepository : Repository<TempleMaster>, ITempleRepository
    {
        private readonly TempleDbContext _context;
        public TempleRepository(TempleDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }        

        public async Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _context.TempleMasters
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TempleMaster?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.TempleMasters
                .Include(t => t.TempleExpertises)
                .Include(t => t.TempleSchedules)
                .Include(t => t.TempleExceptions)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<(List<SearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            string normalizedQuery = query.Trim();

            // Wrap query in double quotes for phrase search in BOOLEAN MODE
            string booleanQuery = $"\"{normalizedQuery}\"";

            int skip = (page - 1) * pageSize;

            var sql = $@"
                        SELECT 
                            a.id AS Id,
                            a.name AS Name,
                            ae.description AS Description,
                            a.thumbnail_url AS ThumbnailUrl,
                            ae.price AS Price,

                            1 AS CatalogAttributeId,
                            1 AS CatalogAttributeValueId,
                            'Test' AS CatalogAttributeValue,
                            'test' AS CatalogAttributeKey,
                            'test' AS CatalogAttributeLabel,
                            1 AS CatalogAttributeDatatype;

                            ae.category_name_snapshot AS CategoryNameSnapshot,
                            ae.sub_category_name_snapshot AS SubCategoryNameSnapshot,
                            ae.category_id AS CategoryId,
                            ae.sub_category_id AS SubcategoryId,

                            LEAST(ROUND(IFNULL(MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS NameScore,
                            LEAST(ROUND(IFNULL(MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS PackageNameScore,
                            LEAST(ROUND(IFNULL(MATCH(ae.category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS CatScore,
                            LEAST(ROUND(IFNULL(MATCH(ae.sub_category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS SubcatScore,

                            (
                                LEAST(ROUND(IFNULL(MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 3 +
                                LEAST(ROUND(IFNULL(MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 3 +
                                LEAST(ROUND(IFNULL(MATCH(ae.category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 2 +
                                LEAST(ROUND(IFNULL(MATCH(ae.sub_category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 1
                            ) AS TotalScore

                        FROM temple_master a
                        LEFT JOIN temple_expertise ae ON a.id = ae.temple_id
                        WHERE 
                            ae.is_active = TRUE AND
                            (
                                MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE) OR
                                MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE) OR
                                MATCH(ae.category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE) OR
                                MATCH(ae.sub_category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE)
                            )
                        ORDER BY TotalScore DESC
                        LIMIT {{1}} OFFSET {{2}};";


            var products = await _context.SearchRaws
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
                            SELECT COUNT(*) FROM temple_master a
                            LEFT JOIN temple_expertise ae ON a.id = ae.temple_id
                            WHERE 
                            MATCH(a.name) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ae.name) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ae.category_name_snapshot) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ae.sub_category_name_snapshot) AGAINST ({0} IN BOOLEAN MODE)";

            var totalCount = await _context.SearchRaws
                .FromSqlRaw(countSql, booleanQuery)
                .CountAsync(cancellationToken);

            return (groupedResults, totalCount);
        }
    }
}