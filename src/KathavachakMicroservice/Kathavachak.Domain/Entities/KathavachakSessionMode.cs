using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public enum SessionModeType
    {
        InPerson = 1,
        Online = 2,
        Hybrid = 3
    }

    public class KathavachakSessionMode
    {
        [Key]
        public int Id { get; set; }

        public int KathavachakId { get; set; }

        public SessionModeType Mode { get; set; }

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
