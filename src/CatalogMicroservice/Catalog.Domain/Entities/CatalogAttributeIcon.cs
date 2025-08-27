using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities
{
    public partial class CatalogAttributeIcon
    {
        [Key]
        public int Id { get; set; }
        // Icon fields for Flutter compatibility
        // Optional friendly name, like "shopping_cart"
        [MaxLength(100)]
        public string? IconName { get; set; }

        // Unicode code point, like 0xe8cc
        public int? IconCodePoint { get; set; }  

        // e.g., Flutter Icon Family "MaterialIcons", "CupertinoIcons"
        [MaxLength(100)]
        public string? IconFontFamily { get; set; }
    }
}