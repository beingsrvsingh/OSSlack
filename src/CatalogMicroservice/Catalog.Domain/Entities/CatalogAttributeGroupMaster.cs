using System.ComponentModel.DataAnnotations;


namespace Catalog.Domain.Entities
{
    public class CatalogAttributeGroupMaster
    {
        [Key]
        public int Id { get; set; }

        // e.g. "basic_info", "nutrient_info"
        [Required, MaxLength(100)]
        public string GroupKey { get; set; } = null!; 

        // e.g. "Basic Information"
        [Required, MaxLength(200)]
        public string DisplayName { get; set; } = null!;

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }

}