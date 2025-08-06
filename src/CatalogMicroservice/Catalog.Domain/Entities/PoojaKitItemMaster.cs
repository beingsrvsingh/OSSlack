using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities
{
    public class PoojaKitItemMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PoojaKitMasterId { get; set; }

        [Required, MaxLength(150)]
        public string ItemName { get; set; } = null!;

        public string? Description { get; set; }

        public int Quantity { get; set; } = 1;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("PoojaKitMasterId")]
        public virtual PoojaKitMaster PoojaKitMaster { get; set; } = null!;
        public ICollection<PoojaKitItemLocalizedText> Localizations { get; set; } = new List<PoojaKitItemLocalizedText>();
    }
}