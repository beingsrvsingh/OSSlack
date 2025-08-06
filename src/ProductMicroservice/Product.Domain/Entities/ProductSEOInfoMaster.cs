using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductSEOInfoMaster
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [MaxLength(150)]
        public string? Slug { get; set; } // e.g., "vedic-kundli-book"

        [MaxLength(150)]
        public string? MetaTitle { get; set; }

        [MaxLength(300)]
        public string? MetaDescription { get; set; }

        [ForeignKey("ProductMasterId")]
        public virtual ProductMaster ProductMaster { get; set; } = null!;
    }
}