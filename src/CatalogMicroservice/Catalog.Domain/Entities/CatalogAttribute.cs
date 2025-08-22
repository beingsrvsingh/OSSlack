using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Enums;

namespace Catalog.Domain.Entities
{
    public class CatalogAttribute
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Key { get; set; } = null!;  // unique key for internal use

        [Required, MaxLength(200)]
        public string Label { get; set; } = null!;  // Display name

        [Required]
        public AttributeDataType DataType { get; set; }

        public bool IsCustom { get; set; } = false;

        public bool IsRequired { get; set; } = false;

        public int SortOrder { get; set; } = 0;
         
        public int? CatalogAttributeIconId { get; set; }

        [ForeignKey(nameof(CatalogAttributeIconId))]
        public virtual CatalogAttributeIcon? CatalogAttributeIcon { get; set; }

        public int SubCategoryMasterId { get; set; }

        // FK to Category
        [ForeignKey(nameof(SubCategoryMasterId))]
        public virtual SubCategoryMaster SubCategoryMaster { get; set; } = null!;

        // For Enum type: Navigation to allowed values
        public virtual ICollection<CatalogAttributeAllowedValue> AllowedValues { get; set; } = new List<CatalogAttributeAllowedValue>();

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }    
}