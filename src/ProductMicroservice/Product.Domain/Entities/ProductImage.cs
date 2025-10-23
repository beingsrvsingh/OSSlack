using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; } = null!;

        public int SortOrder { get; set; } = 0;
        [MaxLength(50)]        
        public string? AltText { get; set; }

        [Required]
        public int ProductId { get; set; }
        
        [ForeignKey(nameof(ProductId))]
        public virtual ProductMaster Product { get; set; } = null!;
    }

}