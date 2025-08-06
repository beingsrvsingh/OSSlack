using System.ComponentModel.DataAnnotations;

namespace StockMicroservice.Domain.Entities
{
    public class StockAlert
    {
        [Key]
        public int Id { get; set; }

        public int StockId { get; set; }

        public int CurrentQuantity { get; set; }

        public int Threshold { get; set; }

        [MaxLength(100)]
        public string Status { get; set; } = "Pending"; // "Pending", "Resolved"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public StockMaster Stock { get; set; } = null!;
    }

}