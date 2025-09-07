using System.ComponentModel.DataAnnotations;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakSchedule
    {
        [Key]
        public int Id { get; set; }

        public int KathavachakId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsRecurring { get; set; }

        public KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
