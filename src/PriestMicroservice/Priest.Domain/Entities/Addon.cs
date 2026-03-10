using PriestMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Priest.Domain.Entities
{
    public class Addon : BaseAddon
    {
        public int? PriestExpertiseId { get; set; }

        [ForeignKey(nameof(PriestExpertiseId))]
        public virtual PriestExpertise? PriestExpertise { get; set; }

        public ICollection<AddOnMedia> AddOnMedias { get; set; } = new List<AddOnMedia>();
    }

}
