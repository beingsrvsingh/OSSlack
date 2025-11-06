using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TempleAddon : BaseAddon
    {
        // Foreign key to the product
        public int? TempleId { get; set; }

        [ForeignKey(nameof(TempleId))]
        public virtual TempleMaster TempleMaster { get; set; } = null!;

        public int? TempleExpertiseId { get; set; }

        [ForeignKey(nameof(TempleExpertiseId))]
        public virtual TempleExpertise? TempleExpertise { get; set; }
    }
}
