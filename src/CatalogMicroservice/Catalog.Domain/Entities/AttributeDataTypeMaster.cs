using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities
{
    public partial class AttributeDataTypeMaster
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        public virtual ICollection<CatalogAttribute> CatalogAttributes { get; set; } = new List<CatalogAttribute>();

    }
}