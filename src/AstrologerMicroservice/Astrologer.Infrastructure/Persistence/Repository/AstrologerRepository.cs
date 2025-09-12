using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Repositories;
using AstrologerMicroservice.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace AstrologerMicroservice.Infrastructure.Persistence.Repository
{
    public class AstrologerRepository : Repository<AstrologerEntity>, IAstrologerRepository
    {
        private readonly AstrologerDbContext _context;
        public AstrologerRepository(AstrologerDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<AstrologerEntity>> GetAvailableAsync(DateTime date, string language, string expertise)
        {
            return await _context.Astrologers
            .Where(a => a.IsActive)
            .Where(a => a.AstrologerLanguages.Any(l => l.Language.Name == language))
            .Where(a => a.Schedules.Any(s => s.Day == date.DayOfWeek))
            .ToListAsync();
        }

        public async Task<IEnumerable<AstrologerEntity>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _context.Astrologers
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<AstrologerEntity?> GetAstrologerWithLanguagesAndExpertisesAsync(int id)
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

                            LEAST(ROUND(IFNULL(MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS NameScore,
                            LEAST(ROUND(IFNULL(MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS PackageNameScore,
                            LEAST(ROUND(IFNULL(MATCH(ae.category_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS CatScore,
                            LEAST(ROUND(IFNULL(MATCH(ae.sub_cat_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) AS SubcatScore,

                            (
                                LEAST(ROUND(IFNULL(MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 3 +
                                LEAST(ROUND(IFNULL(MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 3 +
                                LEAST(ROUND(IFNULL(MATCH(ae.category_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 2 +
                                LEAST(ROUND(IFNULL(MATCH(ae.sub_cat_name_snap) AGAINST ({{0}} IN BOOLEAN MODE), 0), 4), 1000) * 1
                            ) AS TotalScore

                        FROM astrologers a
                        LEFT JOIN astrologer_expertises ae ON a.id = ae.astrologer_id
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


            var products = await _context.AstrologerSearchRaws
                .FromSqlRaw(sql, booleanQuery, pageSize, skip)
                .ToListAsync(cancellationToken);


            var countSql = @"
                            SELECT COUNT(*) FROM astrologers a
                            LEFT JOIN astrologer_expertises ae ON a.id = ae.astrologer_id
                            WHERE 
                            MATCH(a.name) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ae.name) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ae.category_name_snap) AGAINST ({0} IN BOOLEAN MODE)
                            OR MATCH(ae.sub_cat_name_snap) AGAINST ({0} IN BOOLEAN MODE)";

            var totalCount = await _context.AstrologerSearchRaws
                .FromSqlRaw(countSql, booleanQuery)
                .CountAsync(cancellationToken);

            return (products, totalCount);
        }

    }
}