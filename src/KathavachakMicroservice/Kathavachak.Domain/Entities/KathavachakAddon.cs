using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakAddon : BaseAddon
    {
        // Foreign key to the product
        public int? KathavachakId { get; set; }

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster KathavachakMaster { get; set; } = null!;

        public int? KathavachakExpertiseId { get; set; }

        [ForeignKey(nameof(KathavachakExpertiseId))]
        public virtual KathavachakExpertise? KathavachakExpertise { get; set; }
    }
}
