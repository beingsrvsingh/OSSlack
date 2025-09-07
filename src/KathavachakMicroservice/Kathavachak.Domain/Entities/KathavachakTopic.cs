using System.ComponentModel.DataAnnotations;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakTopic
    {
        [Key]
        public int Id { get; set; }

        public int KathavachakId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        public KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
