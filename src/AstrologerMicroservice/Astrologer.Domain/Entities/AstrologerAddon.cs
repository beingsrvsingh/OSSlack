using AstrologerMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Astrologer.Domain.Entities
{
    public class AstrologerAddon : BaseAddon
    {

        public int? AstrologerId { get; set; } 
        public int? AstrologerExpertiseId { get; set; }

        [ForeignKey(nameof(AstrologerId))]
        public virtual AstrologerMaster? Astrologer { get; set; }

        [ForeignKey(nameof(AstrologerExpertiseId))]
        public virtual AstrologerExpertise? AstrologerExpertise { get; set; }
    }

}
