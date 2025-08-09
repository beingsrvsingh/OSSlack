
using StockManagement.Application.Contracts;

namespace StockManagement.Application.Services
{
    public interface IStockAlertService
    {
        Task<IEnumerable<StockAlertDto>> GetActiveAlertsAsync();
        Task<bool> AddAlertAsync(StockAlertDto dto);
    }

}