using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities
{
    public class CatalogAttributeAllowedValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CatalogAttributeId { get; set; }

        [ForeignKey(nameof(CatalogAttributeId))]
        public CatalogAttribute CatalogAttribute { get; set; } = null!;

        // e.g. "Red", "Blue", "Small", "Large"
        [Required, MaxLength(100)]
        public string Value { get; set; } = null!;

        public int SortOrder { get; set; } = 0;

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}