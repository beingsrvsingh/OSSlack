using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pooja.Domain.Entities
{
    public class PoojaAddon : BaseAddon
    {
        // Foreign key to the product
        public int? PoojaId { get; set; }

        [ForeignKey(nameof(PoojaId))]
        public virtual PoojaMaster PoojaMaster { get; set; } = null!;

        public int? PoojaVariantId { get; set; }

        [ForeignKey(nameof(PoojaVariantId))]
        public virtual PoojaVariantMaster? PoojaVariantMaster { get; set; }
    }

}
