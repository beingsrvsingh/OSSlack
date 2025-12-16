using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Polly;
using Priest.Domain.Core.Repository;
using Priest.Infrastructure.Persistence.Context;
using PriestMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
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
                        FROM priest_master p
                        INNER JOIN priest_expertise pv 
                            ON p.id = pv.priest_id
                        WHERE MATCH(pv.name) AGAINST (@q IN NATURAL LANGUAGE MODE)
                        LIMIT @pageSize OFFSET @skip;";

            var products = await dbContext.SearchRaws
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
