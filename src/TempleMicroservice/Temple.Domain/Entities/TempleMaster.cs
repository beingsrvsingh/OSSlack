using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public partial class TempleMaster : CatalogMetadata
    {

        [Required]
        public int LocationId { get; set; }

        // Navigation properties if needed
        public virtual ICollection<TempleExpertise> TempleExpertises { get; set; } = new List<TempleExpertise>();
        public virtual ICollection<TempleSchedule> TempleSchedules { get; set; } = new List<TempleSchedule>();
        public virtual ICollection<TempleException> TempleExceptions { get; set; } = new List<TempleException>();

        // Attributes directly associated with temple (not expertise-specific)
        public virtual ICollection<AttributeValue> TempleAttributes { get; set; } = new List<AttributeValue>();

    }
}