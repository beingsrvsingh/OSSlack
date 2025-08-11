using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities
{
    public class PoojaTag
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string TagName { get; set; } = null!; // e.g., "Festival", "Regional", "Vedic"

        public ICollection<PoojaMaster> Poojas { get; set; } = new List<PoojaMaster>();
    }
}