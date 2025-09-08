using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakAttributeValue : BaseAttributeValue
    {
        [Key]
        public int Id { get; set; }
        public int KathavachakId { get; set; }

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
