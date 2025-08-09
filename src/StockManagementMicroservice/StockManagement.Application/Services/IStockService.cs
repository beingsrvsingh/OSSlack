using StockManagement.Application.Contracts;

namespace StockManagement.Application.Services
{
    public interface IStockService
    {
        Task<StockDto?> GetByIdAsync(int id);
        Task<IEnumerable<StockDto>> GetByWarehouseIdAsync(int warehouseId);
        Task<bool> AddStockAsync(StockDto stockDto);
        Task<bool> UpdateStockAsync(StockDto stockDto);
    }

}