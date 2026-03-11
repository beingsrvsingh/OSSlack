using System.ComponentModel.DataAnnotations;

namespace Order.Domain.Entities
{
    public class OrderShipmentItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ShipmentId { get; set; }
        public virtual OrderShipment Shipment { get; set; } = null!;

        [Required]
        public int OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; } = null!;

        [Required]
        public int QuantityShipped { get; set; } = 1;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

}
