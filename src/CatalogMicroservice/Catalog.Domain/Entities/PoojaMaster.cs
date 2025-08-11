using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities
{
    public partial class PoojaMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SubCategoryMasterId { get; set; }  // FK to SubCategoryMaster (e.g., Ganesh Pooja)

        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;  // Pooja name, e.g., "Ganesh Chaturthi Pooja"

        [MaxLength(1000)]
        public string? Description { get; set; }  // Detailed description

        [MaxLength(300)]
        public string? ImageUrl { get; set; }  // URL to pooja image

        public bool IsActive { get; set; } = true;  // Is this pooja active/offered?

        public bool IsComposite { get; set; } = false; // Does this pooja combine multiple poojas/kits?

        public decimal BasePrice { get; set; }  // Base price for the pooja (could be overridden in product)

        public TimeSpan? Duration { get; set; }  // Approximate duration of the pooja (e.g., 1 hour)

        public int MaxParticipants { get; set; } = 1;  // Max number of participants allowed

        public int DisplayOrder { get; set; } = 0;  // For sorting in UI

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties

        [ForeignKey("SubCategoryMasterId")]
        public virtual SubCategoryMaster SubCategoryMaster { get; set; } = null!;

        public ICollection<PoojaLocalizedText> Localizations { get; set; } = new List<PoojaLocalizedText>();

        public ICollection<PoojaKitMaster> PoojaKits { get; set; } = new List<PoojaKitMaster>();

        // Optional: tags or attributes to filter pooja (e.g., festival-related, regional)
        public ICollection<PoojaTag> Tags { get; set; } = new List<PoojaTag>();
    }

}