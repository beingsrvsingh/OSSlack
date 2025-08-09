
namespace StockManagement.Application.Contracts
{
    public class WarehouseTransferDto
    {
        public int Id { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public DateTime TransferDate { get; set; }
    }

}