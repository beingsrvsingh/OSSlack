using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence.Context;

namespace StockManagement.Infrastructure.Repository
{
    public class StockRepository : Repository<StockMaster>, IStockRepository
    {
        private readonly StockDbContext _context;

        public StockRepository(StockDbContext context) : base(context)
        {
            this._context = context;
        }           

        public async Task<StockMaster?> GetStockByIdAsync(int id) =>
        await _context.Stocks.FindAsync(id);

        public async Task<IEnumerable<StockMaster>> GetByWarehouseIdAsync(int warehouseId) =>
            await _context.Stocks
                .Where(s => s.WarehouseId == warehouseId)
                .ToListAsync();
    }

}