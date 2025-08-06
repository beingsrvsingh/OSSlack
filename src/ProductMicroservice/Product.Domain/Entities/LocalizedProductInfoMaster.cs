using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class LocalizedProductInfoMaster
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required, MaxLength(10)]
        public string LanguageCode { get; set; } = "en"; // e.g., "en", "hi"

        [MaxLength(150)]
        public string? LocalizedName { get; set; }

        public string? LocalizedDescription { get; set; }

        [ForeignKey("ProductMasterId")]
        public virtual ProductMaster ProductMaster { get; set; } = null!;
    }
}