using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities
{
    public class CategoryAttributeGroupMapping
    {
        [Key]
        public int Id { get; set; }

        public int? CategoryMasterId { get; set; }
        public int? SubCategoryMasterId { get; set; }

        public int AttributeGroupId { get; set; }

        [ForeignKey(nameof(AttributeGroupId))]
        public CatalogAttributeGroupMaster AttributeGroup { get; set; } = null!;

        public int SortOrder { get; set; } = 0;
    }

}