
namespace StockManagement.Application.Contracts
{
    public class StockReservationDto
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int QuantityReserved { get; set; }
        public DateTime ReservedUntil { get; set; }
    }

}