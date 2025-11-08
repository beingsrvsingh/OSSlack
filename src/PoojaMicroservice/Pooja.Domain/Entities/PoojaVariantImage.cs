using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pooja.Domain.Entities
{
    public class PoojaVariantImage : BaseMedia
    {
        [Required]
        public int PoojaVariantId { get; set; }

        [ForeignKey(nameof(PoojaVariantId))]
        public virtual PoojaVariantMaster PoojaVariantMaster { get; set; } = null!;
    }
}
