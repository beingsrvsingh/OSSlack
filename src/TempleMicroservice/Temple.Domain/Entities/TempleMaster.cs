using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public partial class TempleMaster : CatalogMetadata
    {
        [Required]
        public int LocationId { get; set; }

        // Navigation properties if needed
        public virtual ICollection<TempleImage> Media { get; set; } = new List<TempleImage>();
        public virtual ICollection<TempleExpertise> VariantMasters { get; set; } = new List<TempleExpertise>();
        public virtual ICollection<TempleSchedule> TempleSchedules { get; set; } = new List<TempleSchedule>();
        public virtual ICollection<TempleException> TempleExceptions { get; set; } = new List<TempleException>();

        // Attributes directly associated with temple (not expertise-specific)
        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();

        public virtual ICollection<TempleAddon> Addons { get; set; } = new List<TempleAddon>();

    }
}