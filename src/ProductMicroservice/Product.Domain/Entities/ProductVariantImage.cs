using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductVariantImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(300)]
        public string ImageUrl { get; set; } = null!;

        public int SortOrder { get; set; } = 0;
        [MaxLength(50)]
        public string? AltText { get; set; }

        [Required]
        public int ProductVariantId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariantMaster ProductVariant { get; set; } = null!;
    }
}
