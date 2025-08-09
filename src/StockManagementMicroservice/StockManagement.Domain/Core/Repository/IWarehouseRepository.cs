using Shared.Domain.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Core.Repository
{
    public interface IWarehouseRepository : IRepository<WarehouseMaster>
    {
        Task<WarehouseMaster?> GetWareshouseByIdAsync(int id);
        Task<IEnumerable<WarehouseMaster>> GetAllWarehouseAsync();
        Task AddWarehouseAsync(WarehouseMaster warehouse);

        Task<IEnumerable<WarehouseTransfer>> GetTransfersByWarehouseIdAsync(int warehouseId);
        Task AddTransferAsync(WarehouseTransfer transfer);
    }

}