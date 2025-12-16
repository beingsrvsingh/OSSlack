using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Repositories;
using AstrologerMicroservice.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Polly;
using Shared.Domain.Entities.Base;
using Shared.Infrastructure.Repositories;

namespace AstrologerMicroservice.Infrastructure.Persistence.Repository
{
    public class AstrologerRepository : Repository<AstrologerMaster>, IAstrologerRepository
    {
        private readonly AstrologerDbContext _context;
        public AstrologerRepository(AstrologerDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<AstrologerMaster>> GetAvailableAsync(DateTime date, string language, string expertise)
        {
            return await _context.Astrologers
            .Where(a => a.IsActive)
            .Where(a => a.AstrologerLanguages.Any(l => l.LanguageName == language))
            .Where(a => a.Schedules.Any(s => s.Day == date.DayOfWeek))
            .ToListAsync();
        }

        public async Task<IEnumerable<AstrologerMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _context.Astrologers
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<AstrologerMaster?> GetAstrologerWithLanguagesAndExpertisesAsync(int id)
        {
            return await _context.Astrologers
                .Include(a => a.AstrologerLanguages)
                .Include(a => a.AstrologerExpertises)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<(List<AstrologerSearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
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
                        FROM astrologer_master p
                        INNER JOIN astrologer_expertise pv 
                            ON p.id = pv.astrologer_id
                        WHERE MATCH(pv.name) AGAINST (@q IN NATURAL LANGUAGE MODE)
                        LIMIT @pageSize OFFSET @skip;";

            var products = await _context.AstrologerSearchRaws
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