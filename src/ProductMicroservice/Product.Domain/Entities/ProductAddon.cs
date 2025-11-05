using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductAddon : BaseAddon
    {
        // Foreign key to the product
        public int? ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual ProductMaster Product { get; set; } = null!;

        public int? ProductVariantId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariantMaster? ProductVariantMaster { get; set; }
    }
}
