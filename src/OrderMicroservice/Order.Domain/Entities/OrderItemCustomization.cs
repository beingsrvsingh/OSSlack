using System.ComponentModel.DataAnnotations;

namespace Order.Domain.Entities
{
    public class OrderItemCustomization
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderItemId { get; set; }  // FK to OrderItem

        [Required, MaxLength(100)]
        public string OptionName { get; set; } = null!;  // e.g. "Color", "Size", "Engraving"

        [Required, MaxLength(500)]
        public string OptionValue { get; set; } = null!;  // e.g. "Red", "XL", "Happy Birthday"

        [MaxLength(1000)]
        public string? AdditionalNotes { get; set; }  // Optional extra notes or instructions

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        // Navigation property
        public virtual OrderItem OrderItem { get; set; } = null!;        

    }

}