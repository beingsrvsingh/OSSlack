using AstrologerMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class AstrologerAttributeValue : BaseAttributeValue
    {
        [Key]
        public int Id { get; set; }
        public int AstrologerId { get; set; }

        [ForeignKey(nameof(AstrologerId))]
        public virtual AstrologerEntity Astrologer { get; set; } = null!;
    }

}
