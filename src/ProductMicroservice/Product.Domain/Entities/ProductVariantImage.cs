using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductVariantImage : BaseMedia
    {
        [Required]
        public int ProductVariantId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariantMaster ProductVariant { get; set; } = null!;
    }
}
