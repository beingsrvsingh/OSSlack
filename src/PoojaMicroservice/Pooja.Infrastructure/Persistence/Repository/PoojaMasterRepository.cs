using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Polly;
using Pooja.Domain.Core.Repository;
using Pooja.Domain.Entities;
using Pooja.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace Pooja.Infrastructure.Persistence.Repository
{
    public class PoojaMasterRepository : Repository<PoojaMaster>, IPoojaMasterRepository
    {
        private readonly PoojaDbContext _dbContext;

        public PoojaMasterRepository(PoojaDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<PoojaMaster?> GetByIdAsync(int id) =>
        await _dbContext.PoojaMasters
            .Include(p => p.Addons)
            .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<PoojaMaster>> GetAllAsync() =>
            await _dbContext.PoojaMasters.ToListAsync();

        public async Task AddAsync(PoojaMaster pooja)
        {
            _dbContext.PoojaMasters.Add(pooja);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PoojaMaster pooja)
        {
            _dbContext.PoojaMasters.Update(pooja);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pooja = await _dbContext.PoojaMasters.FindAsync(id);
            if (pooja != null)
            {
                _dbContext.PoojaMasters.Remove(pooja);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PoojaMaster>> GetByTempleAsync(int templeId) =>
            await _dbContext.PoojaMasters
                //.Where(p => p.PoojaTemples.Any(pt => pt.TempleId == templeId))
                .ToListAsync();

        public async Task<IEnumerable<PoojaMaster>> GetByPriestAsync(int priestId) =>
            await _dbContext.PoojaMasters
                //.Where(p => p.PoojaPriests.Any(pp => pp.PriestId == priestId))
                .ToListAsync();


        public async Task<(List<PoojaSearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
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
                        FROM pooja_master p
                        INNER JOIN pooja_variant_master pv 
                            ON p.id = pv.pooja_master_id
                        WHERE MATCH(pv.name) AGAINST (@q IN NATURAL LANGUAGE MODE)
                        LIMIT @pageSize OFFSET @skip;";

            var products = await _dbContext.PoojaSearchRaws
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
