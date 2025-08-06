using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductAttributeMaster
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required, MaxLength(50)]
        public string Key { get; set; } = null!; // e.g., "Material"

        [MaxLength(150)]
        public string? Value { get; set; } // e.g., "Brass"

        [ForeignKey("ProductMasterId")]
        public virtual ProductMaster ProductMaster { get; set; } = null!;
    }
}