using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Priest.Domain.Entities
{
    public class AddOnMedia : BaseMedia
    {
        public int? AddonId { get; set; }

        [ForeignKey(nameof(AddonId))]
        public virtual Addon? Addon { get; set; }
    }
}
