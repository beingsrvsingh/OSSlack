using StockManagement.Application.Contracts;

namespace StockManagement.Application.Services
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseDto>> GetAllAsync();
        Task<WarehouseDto?> GetByIdAsync(int id);
        Task<bool> AddAsync(WarehouseDto dto);

        Task<IEnumerable<WarehouseTransferDto>> GetTransfersAsync(int warehouseId);
        Task<bool> AddTransferAsync(WarehouseTransferDto dto);
    }

}