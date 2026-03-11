using System.ComponentModel.DataAnnotations;

namespace Order.Domain.Entities
{
    public class OrderShipment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }
        public virtual OrderHeader OrderHeader { get; set; } = null!;

        [MaxLength(50)]
        public string? ShippingMethod { get; set; }

        [MaxLength(50)]
        public string? CarrierName { get; set; }

        [MaxLength(100)]
        public string? TrackingNumber { get; set; }

        public DateTime? ShippedDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        [MaxLength(50)]
        public string ShipmentStatus { get; set; } = "Pending";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public virtual ICollection<OrderShipmentItem> ShipmentItems { get; set; } = new List<OrderShipmentItem>();
    }

}
