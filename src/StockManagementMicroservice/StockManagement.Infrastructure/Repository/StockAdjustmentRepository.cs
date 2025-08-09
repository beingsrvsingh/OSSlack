using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence.Context;

namespace StockManagement.Infrastructure.Repository
{
    public class StockAdjustmentRepository : Repository<StockAdjustment>, IStockAdjustmentRepository
    {
        private readonly StockDbContext _context;

        public StockAdjustmentRepository(StockDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<StockAdjustment>> GetByStockIdAsync(int stockId) =>
            await _context.StockAdjustments
                .Where(a => a.StockId == stockId)
                .ToListAsync();

        public async Task AddAdjustmentAsync(StockAdjustment adjustment) =>
            await _context.StockAdjustments.AddAsync(adjustment);

        public async Task<IEnumerable<StockReservation>> GetReservationsByStockIdAsync(int stockId) =>
            await _context.StockReservations
                .Where(r => r.StockId == stockId)
                .ToListAsync();

        public async Task AddReservationAsync(StockReservation reservation) =>
            await _context.StockReservations.AddAsync(reservation);
    }

}