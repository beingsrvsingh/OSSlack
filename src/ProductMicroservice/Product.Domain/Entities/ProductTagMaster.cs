using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductTagMaster
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required, MaxLength(50)]
        public string Tag { get; set; } = null!;

        [ForeignKey("ProductMasterId")]
        public virtual ProductMaster ProductMaster { get; set; } = null!;
    }
}