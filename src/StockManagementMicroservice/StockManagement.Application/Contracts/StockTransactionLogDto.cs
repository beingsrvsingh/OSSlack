
namespace StockManagement.Application.Contracts
{
    public class StockTransactionLogDto
    {
        public int Id { get; set; }
        public int StockTransactionId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

}