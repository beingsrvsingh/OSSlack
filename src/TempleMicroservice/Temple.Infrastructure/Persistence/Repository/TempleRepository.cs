using Microsoft.EntityFrameworkCore;
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

                            ae.category_name_snapshot AS CatSnap,
                            ae.sub_category_name_snapshot AS SubcatSnap,
                            ae.category_id AS CategoryId,
                            ae.sub_category_id AS SubcategoryId,

                            MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE) AS NameScore,
                            MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE) AS PackageNameScore,
                            MATCH(ae.category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE) AS CatScore,
                            MATCH(ae.sub_category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE) AS SubcatScore,

                            (
                                MATCH(a.name) AGAINST ({{0}} IN BOOLEAN MODE) * 3 +
                                MATCH(ae.name) AGAINST ({{0}} IN BOOLEAN MODE) * 3 +
                                MATCH(ae.category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE) * 2 +
                                MATCH(ae.sub_category_name_snapshot) AGAINST ({{0}} IN BOOLEAN MODE) * 1
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

            return (products, totalCount);
        }
    }
}