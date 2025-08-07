using System.ComponentModel.DataAnnotations;
using StockManagementMicroservice.Domain.Enums;

namespace StockManagementMicroservice.Domain.Entities
{
    public class WarehouseTransfer
    {
        [Key]
        public int Id { get; set; }

        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public DateTime TransferDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public TransferStatus Status { get; set; } = TransferStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public virtual WarehouseMaster FromWarehouse { get; set; } = null!;
        public virtual WarehouseMaster ToWarehouse { get; set; } = null!;
    }
}