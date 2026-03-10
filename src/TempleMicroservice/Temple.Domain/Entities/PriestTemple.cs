using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class PriestTemple
    {
        [Key]
        public int Id { get; set; }

        public int PriestId { get; set; }

        public int TempleId { get; set; }

        public string PriestNameSnapshot { get; set; }

        public DateTime AssignedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
