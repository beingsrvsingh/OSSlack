
namespace StockManagement.Application.Contracts
{
    public class StockAlertDto
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string AlertMessage { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}