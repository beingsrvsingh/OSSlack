
using Shared.Domain.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Core.Repository
{
    public interface IStockAlertRepository : IRepository<StockAlert>
    {
        Task<IEnumerable<StockAlert>> GetActiveAlertsAsync();
        Task AddAlertAsync(StockAlert alert);
    }

}