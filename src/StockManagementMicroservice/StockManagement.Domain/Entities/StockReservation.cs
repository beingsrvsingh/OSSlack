using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities
{
    public class StockReservation
    {
        [Key]
        public int Id { get; set; }

        public int StockId { get; set; }

        public int Quantity { get; set; }

        public string ReservedBy { get; set; } = null!; // User or Order ref

        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiresAt { get; set; }

        public StockMaster Stock { get; set; } = null!;
    }

}