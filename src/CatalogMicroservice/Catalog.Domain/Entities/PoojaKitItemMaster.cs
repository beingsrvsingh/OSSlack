using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities
{
    public class PoojaKitItemMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KitSubcategoryId { get; set; }

        [Required]
        public int ProductSubcategoryId { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        [ForeignKey(nameof(KitSubcategoryId))]
        public SubCategoryMaster KitSubcategory { get; set; } = null!;

        [ForeignKey(nameof(ProductSubcategoryId))]
        public SubCategoryMaster ProductSubcategory { get; set; } = null!;
    }

}