
namespace StockManagement.Application.Contracts
{
    public class StockTransactionDto
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string TransactionType { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime Timestamp { get; set; }
    }

}