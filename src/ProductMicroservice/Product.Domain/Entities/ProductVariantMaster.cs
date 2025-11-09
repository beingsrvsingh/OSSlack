using Shared.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductVariantMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int ProductMasterId { get; set; }

        [ForeignKey(nameof(ProductMasterId))]
        public virtual ProductMaster ProductMaster { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public BasePrice Price { get; set; }

        public int? StockQuantity { get; set; }

        public bool IsDefault { get; set; } = false;

        public virtual ICollection<ProductVariantImage> VariantImages { get; set; } = new List<ProductVariantImage>();
        public virtual ICollection<ProductAttributeValue> Attributes { get; set; } = new List<ProductAttributeValue>();

        public virtual ICollection<ProductAddon> ProductAddons { get; set; } = new List<ProductAddon>();
    }
}