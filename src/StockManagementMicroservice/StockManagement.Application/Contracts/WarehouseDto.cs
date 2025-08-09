
namespace StockManagement.Application.Contracts
{
    public class WarehouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
    }

}