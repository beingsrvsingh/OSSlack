using AstrologerMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Astrologer.Domain.Entities
{
    public class AstrologerExpertiesMedia : BaseMedia
    {
        [Required]
        public int AstrolgerExpertiesId { get; set; }

        [ForeignKey(nameof(AstrolgerExpertiesId))]
        public virtual AstrologerExpertise AstrologerExpertise { get; set; } = null!;
    }
}
