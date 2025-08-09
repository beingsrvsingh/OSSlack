
namespace StockManagement.Application.Contracts
{
    public class StockDto
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantityReserved { get; set; }
        public int QuantityInTransit { get; set; }
        public DateTime LastUpdated { get; set; }
    }

}