
using StockManagement.Application.Contracts;

namespace StockManagement.Application.Services
{
    public interface IStockAdjustmentService
    {
        Task<IEnumerable<StockAdjustmentDto>> GetAdjustmentsAsync(int stockId);
        Task<bool> AddAdjustmentAsync(StockAdjustmentDto dto);

        Task<IEnumerable<StockReservationDto>> GetReservationsAsync(int stockId);
        Task<bool> AddReservationAsync(StockReservationDto dto);
    }

}