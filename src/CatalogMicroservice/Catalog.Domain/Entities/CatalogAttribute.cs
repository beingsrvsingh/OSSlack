using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Enums;

namespace Catalog.Domain.Entities
{
    public partial class CatalogAttribute
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string CatalogAttributeKey { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Label { get; set; } = null!;
        
        [Required]
        public string AllowedValuesSource { get; set; } = null!;

        public bool IsCustom { get; set; } = false;
        public bool IsRequired { get; set; } = false;
        public bool IsFilterable { get; set; } = false;
        public bool IsSummary { get; set; } = false;

        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public int AttributeDataTypeId { get; set; }

        public int? AttributeIconId { get; set; }

        [ForeignKey(nameof(AttributeIconId))]
        public virtual CatalogAttributeIcon? AttributeIcon { get; set; }

        [ForeignKey(nameof(AttributeDataTypeId))]
        public virtual AttributeDataTypeMaster AttributeDataType { get; set; } = null!;

        public virtual ICollection<CatalogAttributeAllowedValue> AllowedValues { get; set; } = new List<CatalogAttributeAllowedValue>();
    }

}