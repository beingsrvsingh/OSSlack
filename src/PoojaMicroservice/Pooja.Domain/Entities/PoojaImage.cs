using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pooja.Domain.Entities
{
    public class PoojaImage : BaseMedia
    {

        [Required]
        public int PoojaId { get; set; }

        [ForeignKey(nameof(PoojaId))]
        public virtual PoojaMaster PoojaMaster { get; set; } = null!;
    }
}
