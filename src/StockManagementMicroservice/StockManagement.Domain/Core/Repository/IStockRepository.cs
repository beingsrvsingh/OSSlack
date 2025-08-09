using Shared.Domain.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Core.Repository
{
    public interface IStockRepository : IRepository<StockMaster>
    {
        Task<StockMaster?> GetStockByIdAsync(int id);
        Task<IEnumerable<StockMaster>> GetByWarehouseIdAsync(int warehouseId);
    }
}