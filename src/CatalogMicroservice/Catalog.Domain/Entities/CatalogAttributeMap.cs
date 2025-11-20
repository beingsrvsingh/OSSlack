using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities
{
    public class CatalogAttributeMap
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int AttributeId { get; set; }
        public int AttributeGroupId { get; set; }

        public int SortOrder { get; set; }
        public bool IsRequired { get; set; }
        public bool IsFilterable { get; set; }
        public bool IsSummary { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(AttributeId))]
        public CatalogAttribute Attribute { get; set; }

        [ForeignKey(nameof(AttributeGroupId))]
        public CatalogAttributeGroupMaster AttributeGroup { get; set; }
    }


}
