using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities
{
    public partial class CatalogAttributeIcon
    {
        [Key]
        public int Id { get; set; }
        // Icon fields for Flutter compatibility
        [MaxLength(100)]
        public string? IconName { get; set; }  // Optional friendly name, like "shopping_cart"

        public int? IconCodePoint { get; set; }  // Unicode code point, like 0xe8cc

        [MaxLength(100)]
        public string? IconFontFamily { get; set; }  // e.g., "MaterialIcons", "CupertinoIcons"
    }
}