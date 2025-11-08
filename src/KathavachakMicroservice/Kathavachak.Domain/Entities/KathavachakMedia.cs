using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakMedia : BaseMedia
    {
        public int? KathavachakId { get; set; }

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster? KathavachakMaster { get; set; }
    }

}
