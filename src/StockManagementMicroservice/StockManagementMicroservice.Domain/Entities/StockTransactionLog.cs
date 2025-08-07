using System.ComponentModel.DataAnnotations;
using StockManagementMicroservice.Domain.Enums;

namespace StockManagementMicroservice.Domain.Entities
{
    public class StockTransactionLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StockId { get; set; }

        public int ChangeQuantity { get; set; }  // Positive or negative

        [Required, MaxLength(50)]
        public StockTransactionType TransactionType { get; set; }  // e.g. "Purchase", "Sale", "Return", etc.

        public int? ReferenceId { get; set; }  // Optional: order, return, adjustment ID, etc.

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        // Usually no navigation prop here for simpler archival structure
    }


}