using PriestMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Priest.Domain.Entities
{
    public class PriestMedia : BaseMedia
    {
        public int? PriestId { get; set; }

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster? PriestMaster { get; set; }
    }
}
