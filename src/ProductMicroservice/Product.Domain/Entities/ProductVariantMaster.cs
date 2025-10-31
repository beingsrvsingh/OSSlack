using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductVariantMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProductMasterId { get; set; }

        [ForeignKey(nameof(ProductMasterId))]
        public virtual ProductMaster ProductMaster { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
        public decimal? MRP { get; set; }
        public int? StockQuantity { get; set; }
        public bool IsDefault { get; set; } = false;

        public int? DurationMinutes { get; set; }
        public int? AvailableSlots { get; set; }
        public string? BookingType { get; set; }

        public virtual ICollection<ProductVariantImage> VariantImages { get; set; } = new List<ProductVariantImage>();
        public virtual ICollection<ProductAttributeValue> Attributes { get; set; } = new List<ProductAttributeValue>();
    }
}