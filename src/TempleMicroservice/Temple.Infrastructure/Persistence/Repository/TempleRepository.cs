using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Shared.Domain.Entities.Base;
using Shared.Infrastructure.Repositories;
using System.Linq.Expressions;
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

        public async Task<List<TempleMaster>> GetAsync(Expression<Func<TempleMaster, bool>> predicate, Func<IQueryable<TempleMaster>, IQueryable<TempleMaster>>? include = null)
        {
            IQueryable<TempleMaster> query = _context.TempleMasters;

            if (include != null)
                query = include(query);

            return await query.AsNoTracking().Where(predicate).ToListAsync();
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
                .Include(t => t.VariantMasters)
                .Include(t => t.TempleSchedules)
                .Include(t => t.TempleExceptions)
                .FirstOrDefaultAsync(t => t.Id == id);
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
                        FROM temple_master p
                        INNER JOIN temple_expertise pv 
                            ON p.id = pv.temple_id
                        WHERE MATCH(pv.name) AGAINST (@q IN NATURAL LANGUAGE MODE)
                        LIMIT @pageSize OFFSET @skip;";

            var products = await _context.SearchRaws
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