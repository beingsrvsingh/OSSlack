using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
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
                            km.description AS Description,
                            km.thumbnail_url AS ThumbnailUrl,
                            km.price AS Price,
                            ke.cat_snap AS CatSnap,
                            ke.subcat_snap AS SubcatSnap,
                            ke.cat_id AS CategoryId,
                            ke.subcat_id AS SubcategoryId,
                            MATCH(km.name) AGAINST ({{0}} IN BOOLEAN MODE) AS NameScore,
                            MATCH(ke.cat_snap) AGAINST ({{0}} IN BOOLEAN MODE) AS CatScore,
                            MATCH(ke.subcat_snap) AGAINST ({{0}} IN BOOLEAN MODE) AS SubcatScore,
                            (
                                MATCH(km.name) AGAINST ({{0}} IN BOOLEAN MODE) * 3 +
                                MATCH(ke.cat_snap) AGAINST ({{0}} IN BOOLEAN MODE) * 2 +
                                MATCH(ke.subcat_snap) AGAINST ({{0}} IN BOOLEAN MODE) * 1
                            ) AS TotalScore
                        FROM kathavachak_master km
                        LEFT JOIN kathavachak_experties ke ON km.id = ke.kathavachak_id
                        WHERE 
                            MATCH(km.name) AGAINST ({{0}} IN BOOLEAN MODE)
                            OR MATCH(ke.cat_snap) AGAINST ({{0}} IN BOOLEAN MODE)
                            OR MATCH(ke.subcat_snap) AGAINST ({{0}} IN BOOLEAN MODE)
                        ORDER BY TotalScore DESC
                        LIMIT {{1}} OFFSET {{2}};";

            var products = await _context.KathavachakSearchRaws
                .FromSqlRaw(sql, booleanQuery, pageSize, skip)
                .ToListAsync(cancellationToken);


            var countSql = @"
                            SELECT COUNT(*) FROM product_master
                            WHERE 
                            MATCH(name) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(cat_snap) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(subcat_snap) AGAINST ({0} IN BOOLEAN MODE)";

            var totalCount = await _context.KathavachakSearchRaws
                .FromSqlRaw(countSql, booleanQuery)
                .CountAsync(cancellationToken);

            return (products, totalCount);
        }
    }
}
