using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductVariantMaster
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        [ForeignKey("ProductMasterId")]
        public virtual ProductMaster ProductMaster { get; set; } = null!;
    }
}