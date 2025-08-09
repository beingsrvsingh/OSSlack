using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;
using StockManagement.Domain.Core.Repository;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence.Context;

namespace StockManagement.Infrastructure.Repository
{
    public class StockAlertRepository : Repository<StockAlert>, IStockAlertRepository
    {
        private readonly StockDbContext _context;

        public StockAlertRepository(StockDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<StockAlert>> GetActiveAlertsAsync() =>
        await _context.StockAlerts
            .Where(a => a.IsActive)
            .ToListAsync();

        public async Task AddAlertAsync(StockAlert alert) =>
            await _context.StockAlerts.AddAsync(alert);
    }

}