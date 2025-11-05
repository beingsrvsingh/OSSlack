using AstrologerMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Astrologer.Domain.Entities
{
    public class AstrologerMedia : BaseMedia
    {
        public int? AstrologerId { get; set; }

        [ForeignKey(nameof(AstrologerId))]
        public virtual AstrologerMaster? Astrologer { get; set; }
    }
}
