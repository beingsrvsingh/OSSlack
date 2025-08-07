using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities
{
    public class StockTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StockId { get; set; }

        public int QuantityChanged { get; set; }  // Positive or negative

        [Required, MaxLength(50)]
        public string ActionType { get; set; } = null!; // e.g. "IN", "OUT", "RESERVE", "ADJUST", "RETURN"

        [MaxLength(250)]
        public string? Notes { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public virtual StockMaster Stock { get; set; } = null!;
    }

}