using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence.Context;

namespace StockManagement.Infrastructure.Repository
{
    public class WarehouseRepository : Repository<WarehouseMaster>, IWarehouseRepository
    {
        private readonly StockDbContext _context;

        public WarehouseRepository(StockDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<WarehouseMaster?> GetWareshouseByIdAsync(int id) =>
        await _context.Warehouses.FindAsync(id);

        public async Task<IEnumerable<WarehouseMaster>> GetAllWarehouseAsync() =>
            await _context.Warehouses.ToListAsync();

        public async Task AddWarehouseAsync(WarehouseMaster warehouse) =>
            await _context.Warehouses.AddAsync(warehouse);

        public async Task<IEnumerable<WarehouseTransfer>> GetTransfersByWarehouseIdAsync(int warehouseId) =>
            await _context.WarehouseTransfers
                .Where(t => t.FromWarehouseId == warehouseId || t.ToWarehouseId == warehouseId)
                .ToListAsync();

        public async Task AddTransferAsync(WarehouseTransfer transfer) =>
            await _context.WarehouseTransfers.AddAsync(transfer);
    }

}