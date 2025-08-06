using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductRegionPriceMaster
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required, MaxLength(10)]
        public string RegionCode { get; set; } = "IN";
        public decimal Price { get; set; }
        [ForeignKey("ProductMasterId")]
        public virtual ProductMaster ProductMaster { get; set; } = null!;
    }
}