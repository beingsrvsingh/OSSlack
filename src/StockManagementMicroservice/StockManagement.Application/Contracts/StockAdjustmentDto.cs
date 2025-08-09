
namespace StockManagement.Application.Contracts
{
    public class StockAdjustmentDto
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int AdjustedQuantity { get; set; }
        public string Reason { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

}