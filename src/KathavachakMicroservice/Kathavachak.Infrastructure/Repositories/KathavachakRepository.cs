using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities.Base;
using Shared.Infrastructure.Repositories;

namespace Kathavachak.Infrastructure.Repositories
{
    public class KathavachakRepository : Repository<KathavachakMaster>, IKathavachakRepository
    {
        private readonly KathavachakDbContext _context;

        public KathavachakRepository(KathavachakDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<(List<KathavachakSearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            string normalizedQuery = query.Trim();

            // Wrap query in double quotes for phrase search in BOOLEAN MODE
            string booleanQuery = $"\"{normalizedQuery}\"";

            int skip = (page - 1) * pageSize;

            var sql = $@"
                        SELECT 
                            km.id AS Id,
                            km.name AS Name,
                            ke.description AS Description,
                            km.thumbnail_url AS ThumbnailUrl,
                            ke.price AS Price,
                            ke.category_name_snap AS CatSnap,
                            ke.sub_cat_name_snap AS SubcatSnap,
                            ke.cat_id AS CategoryId,
                            ke.subcat_id AS SubcategoryId,

                            LEAST(ROUND(IFNULL(MATCH(km.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS NameScore,
                            LEAST(ROUND(IFNULL(MATCH(ke.category_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS CatScore,
                            LEAST(ROUND(IFNULL(MATCH(ke.sub_cat_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS SubcatScore,
                            (
                                LEAST(ROUND(IFNULL(MATCH(km.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 3 +
                                LEAST(ROUND(IFNULL(MATCH(ke.category_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 2 +
                                LEAST(ROUND(IFNULL(MATCH(ke.sub_cat_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 1
                            ) AS TotalScore

                        FROM kathavachak_master km
                        LEFT JOIN kathavachak_experties ke ON km.id = ke.kathavachak_id

                        WHERE 
                            IFNULL(MATCH(km.name) AGAINST ({{0}} IN BOOLEAN MODE), 0) > 0 OR
                            IFNULL(MATCH(ke.category_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0) > 0 OR
                            IFNULL(MATCH(ke.sub_cat_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0) > 0

                        ORDER BY TotalScore DESC
                        LIMIT {{1}} OFFSET {{2}};";

            var products = await _context.KathavachakSearchRaws
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
                                            CategoryNameSnapshot = x.CategoryNameSnapshot,
                                            CatalogAttributeId = x.CatalogAttributeId,
                                            CatalogAttributeValueId = x.CatalogAttributeValueId,
                                            Value = x.CatalogAttributeValue,
                                            AttributeKey = x.CatalogAttributeKey,
                                            AttributeLabel = x.CatalogAttributeLabel,
                                            AttributeDataTypeId = x.CatalogAttributeDatatype
                                        })
                                        .ToList();

                                    return product;
                                })
                                .ToList();


            var countSql = @"
                            SELECT COUNT(*) FROM kathavachak_master as km
                            LEFT JOIN kathavachak_experties ke ON km.id = ke.kathavachak_id
                            WHERE 
                            MATCH(km.name) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ke.category_name_snap) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ke.sub_cat_name_snap) AGAINST ({0} IN BOOLEAN MODE)";

            var totalCount = await _context.KathavachakSearchRaws
                .FromSqlRaw(countSql, booleanQuery)
                .CountAsync(cancellationToken);

            return (groupedResults, totalCount);
        }
    }
}
