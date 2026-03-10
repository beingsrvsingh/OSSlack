using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public partial class TempleMaster : CatalogMetadata
    {
        [Required]
        public int LocationId { get; set; }

        // Navigation properties if needed
        public virtual ICollection<TempleMedia> Media { get; set; } = new List<TempleMedia>();
        public virtual ICollection<TempleExpertise> VariantMasters { get; set; } = new List<TempleExpertise>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<TempleException> TempleExceptions { get; set; } = new List<TempleException>();

        // Attributes directly associated with temple (not expertise-specific)
        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();

        public virtual ICollection<TempleAddon> Addons { get; set; } = new List<TempleAddon>();
        public virtual ICollection<ScheduleException> ScheduleExceptions { get; set; } = new List<ScheduleException>();

        public virtual ICollection<PriestTempleService> PriestServices { get; set; } = new List<PriestTempleService>();
    }
}