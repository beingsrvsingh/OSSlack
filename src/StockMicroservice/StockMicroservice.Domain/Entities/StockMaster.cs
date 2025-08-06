using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMicroservice.Domain.Entities
{
    public class StockMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; } // From Product Microservice

        [Required]
        public int WarehouseId { get; set; }

        [Required]
        public int QuantityAvailable { get; set; }

        public int QuantityReserved { get; set; } = 0;

        public int QuantityDamaged { get; set; } = 0;

        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public WarehouseMaster Warehouse { get; set; } = null!;
    }

}