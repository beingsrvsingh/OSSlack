using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Catalog.Domain.Entities
{
    public class PoojaKitMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SubCategoryMasterId { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("SubCategoryMasterId")]
        public virtual SubCategoryMaster SubCategoryMaster { get; set; } = null!;

        public virtual ICollection<PoojaKitItemMaster> PoojaKitItems { get; set; } = new List<PoojaKitItemMaster>();
        public ICollection<PoojaKitLocalizedText> Localizations { get; set; } = new List<PoojaKitLocalizedText>();

    }
}