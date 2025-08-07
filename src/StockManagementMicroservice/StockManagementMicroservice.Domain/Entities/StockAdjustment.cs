using System.ComponentModel.DataAnnotations;
using StockManagementMicroservice.Domain.Enums;

namespace StockManagementMicroservice.Domain.Entities
{
    public class StockAdjustment
    {
        [Key]
        public int Id { get; set; }

        public int StockId { get; set; }

        public StockAdjustmentType AdjustmentType { get; set; }

        public int Quantity { get; set; }
        public StockAlertStatus Status { get; set; } = StockAlertStatus.Pending;


        [MaxLength(500)]
        public string? Reason { get; set; }

        public DateTime AdjustmentDate { get; set; } = DateTime.UtcNow;

        public virtual StockMaster Stock { get; set; } = null!;
    }
}