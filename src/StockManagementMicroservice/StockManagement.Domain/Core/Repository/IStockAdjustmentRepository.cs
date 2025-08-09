using Shared.Domain.Repository;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Core.Repository
{
    public interface IStockAdjustmentRepository : IRepository<StockAdjustment>
    {
        Task<IEnumerable<StockAdjustment>> GetByStockIdAsync(int stockId);
        Task AddAdjustmentAsync(StockAdjustment adjustment);

        Task<IEnumerable<StockReservation>> GetReservationsByStockIdAsync(int stockId);
        Task AddReservationAsync(StockReservation reservation);
    }

}