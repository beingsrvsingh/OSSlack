using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Temple.Domain.Entities;

namespace Temple.Domain.Entities
{
    public class TempleImage : BaseMedia
    {
        [Required]
        public int TempleId { get; set; }
        
        [ForeignKey(nameof(TempleId))]
        public virtual TempleMaster TempleMaster { get; set; } = null!;
    }

}