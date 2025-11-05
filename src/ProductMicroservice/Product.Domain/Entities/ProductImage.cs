using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductImage : BaseMedia
    {

        [Required]
        public int ProductId { get; set; }
        
        [ForeignKey(nameof(ProductId))]
        public virtual ProductMaster Product { get; set; } = null!;
    }

}