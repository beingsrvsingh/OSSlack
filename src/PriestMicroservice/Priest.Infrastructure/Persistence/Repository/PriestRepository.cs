using Microsoft.EntityFrameworkCore;
using Priest.Domain.Core.Repository;
using Priest.Infrastructure.Persistence.Context;
using PriestMicroservice.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class PriestRepository : Repository<PriestMaster>, IPriestRepository
    {
        private readonly PriestDbContext dbContext;

        public PriestRepository(PriestDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
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

                            ae.category_name_snap AS CatSnap,
                            ae.sub_cat_name_snap AS SubcatSnap,
                            ae.category_id AS CategoryId,
                            ae.sub_category_id AS SubcategoryId,

                            MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE) AS NameScore,
                            MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE) AS PackageNameScore,
                            MATCH(ae.category_name_snap) AGAINST ({{0}} IN BOOLEAN MODE) AS CatScore,
                            MATCH(ae.sub_cat_name_snap) AGAINST ({{0}} IN BOOLEAN MODE) AS SubcatScore,

                            (
                                MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE) * 3 +
                                MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE) * 3 +
                                MATCH(ae.category_name_snap) AGAINST ({{0}} IN BOOLEAN MODE) * 2 +
                                MATCH(ae.sub_cat_name_snap) AGAINST ({{0}} IN BOOLEAN MODE) * 1
                            ) AS TotalScore

                        FROM priests a
                        LEFT JOIN priest_expertises ae ON a.id = ae.priest_id
                        WHERE 
                            ae.is_active = TRUE AND
                            (
                                MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE) OR
                                MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE) OR
                                MATCH(ae.category_name_snap) AGAINST ({{0}} IN BOOLEAN MODE) OR
                                MATCH(ae.sub_cat_name_snap) AGAINST ({{0}} IN BOOLEAN MODE)
                            )
                        ORDER BY TotalScore DESC
                        LIMIT {{1}} OFFSET {{2}};";


            var products = await dbContext.SearchRaws
                .FromSqlRaw(sql, booleanQuery, pageSize, skip)
                .ToListAsync(cancellationToken);


            var countSql = @"
                            SELECT COUNT(*) FROM priests a
                            LEFT JOIN priest_expertises ae ON a.id = ae.priest_id
                            WHERE 
                            MATCH(a.name) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ae.name) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ae.category_name_snap) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ae.sub_cat_name_snap) AGAINST ({0} IN BOOLEAN MODE)";

            var totalCount = await dbContext.SearchRaws
                .FromSqlRaw(countSql, booleanQuery)
                .CountAsync(cancellationToken);

            return (products, totalCount);
        }
    }

}
