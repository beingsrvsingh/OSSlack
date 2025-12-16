using Kathavachak.Domain.Core.Repository;
using Kathavachak.Domain.Entities;
using Kathavachak.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
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
                        FROM kathavachak_master p
                        INNER JOIN kathavachak_expertise pv 
                            ON p.id = pv.kathavachak_id
                        WHERE MATCH(pv.name) AGAINST (@q IN NATURAL LANGUAGE MODE)
                        LIMIT @pageSize OFFSET @skip;";

            var products = await _context.KathavachakSearchRaws
                            .FromSqlRaw(
                                sql,
                                new MySqlParameter("@q", escapedQuery),
                                new MySqlParameter("@pageSize", pageSize),
                                new MySqlParameter("@skip", skip)
                            ).ToListAsync(cancellationToken);

            int totalCount = products.FirstOrDefault()?.TotalCount ?? 0;

            return (products, totalCount);
        }
    }
}
