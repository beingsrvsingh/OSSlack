using System.ComponentModel.DataAnnotations;

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

        public KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
